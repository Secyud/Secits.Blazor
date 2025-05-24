using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public static class ComponentsExtensions
{
    public static string? GetBodyName<TItem, TValue>(this Expression<Func<TItem, TValue>> expr)
    {
        var body = expr.Body.ToString();
        var index = body.IndexOf('.') + 1;
        return index <= 0 ? null : body[index..];
    }

    public static void UseParameter<TParameter>(this ParameterView view,
        TParameter previous, string name, Action<TParameter> action)
    {
        if (!view.TryGetValue<TParameter>(name, out var value)) return;
        if (Equals(previous, value)) return;
        action(value);
    }
}