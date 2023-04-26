using System.Text.Json;
using Kira.Security.Shared.Jwt.Extensions;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PRAS.Testovoe.Main.Data.Contexts;
using PRAS.Testovoe.Main.Data.Repositories;
using PRAS.Testovoe.Main.Data.Seeding;
using PRAS.Testovoe.Main.Models;
using PRAS.Testovoe.Main.Services.Auth;
using PRAS.Testovoe.Main.Services.Image;
using PRAS.Testovoe.Main.Services.NewsServices;
using Raf.Security.Shared.Jwt.Options;
using Raf.Utils.Shared.Cookie;
using Raf.Utils.Shared.Time;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(logging => 
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

var jwtOptionsSection = builder.Configuration.GetSection("JwtOptions");
var jwtOptions = jwtOptionsSection.Get<JwtOptions>();
var dbConnectionString = builder.Configuration.GetConnectionString("PrasDb"); 

if (jwtOptions == null)
{
    throw new NullReferenceException(nameof(jwtOptions));
}

builder.Services.Configure<JwtOptions>(jwtOptionsSection);

// db
builder.Services.AddDbContext<NewsDbContext>(options =>
{
    options.UseSqlServer(dbConnectionString,
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                2,
                TimeSpan.FromSeconds(3),
                null);
        });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<NewsDbContext>();

builder.Services.AddCors();

// services
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddTransient<INewsService, NewsService>();
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddScoped<ICookieService, CookieService>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddJwtAuthentication(jwtOptions);
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddJsonOptions(options => 
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; 
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(opt=>{
    opt.AllowAnyHeader();
    opt.AllowAnyMethod();
    opt.WithOrigins("http://localhost:4200");
});

app.UseHttpLogging();

await app.SeedData();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();