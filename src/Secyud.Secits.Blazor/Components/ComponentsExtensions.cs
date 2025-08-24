using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public static class ComponentsExtensions
{
    public static string? GetBodyName<TItem, TValue>(this Expression<Func<TItem, TValue>>? expr)
    {
        if (expr is null) return null;

        var body = expr.Body;
        if (body is UnaryExpression unary)
            body = unary.Operand;

        var name = body.ToString();
        var index = name.IndexOf('.') + 1;
        return index <= 0 ? null : name[index..];
    }

}