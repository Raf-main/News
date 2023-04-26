using System.Linq.Expressions;
using Raf.Infrastructure.Shared.SpecificationPattern;
using PRAS.Testovoe.Main.Models;

namespace PRAS.Testovoe.Main.Services.Auth.Specifications;

public class ByTokenSpecification : Specification<RefreshToken>
{
    private readonly string _token;

    public ByTokenSpecification(string token)
    {
        _token = token;
    }

    public override Expression<Func<RefreshToken, bool>> ToExpression()
    {
        Expression<Func<RefreshToken, bool>> expression = r => r.Token == _token;

        return expression;
    }
}