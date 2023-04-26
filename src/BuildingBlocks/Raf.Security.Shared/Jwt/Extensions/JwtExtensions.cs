using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Raf.Security.Shared.Jwt.Options;
using Raf.Security.Shared.Jwt.Services;

namespace Kira.Security.Shared.Jwt.Extensions;

public static class JwtExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
    {
        services.AddTransient<IJwtService, JwtService>();
        
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RefreshOnIssuerKeyNotFound = false;
            options.RequireHttpsMetadata = false;
            options.IncludeErrorDetails = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = jwtOptions.ValidateIssuer,
                ValidateAudience = jwtOptions.ValidateAudience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                SaveSigninToken = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = jwtOptions.GetSymmetricSecurityKey()
            };
        });
    }
}