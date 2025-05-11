using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public static class ComponentsExtensions
{
    public static string? GetBodyName<TItem, TValue>(this Expression<Func<TItem, TValue>> expr)
    {
        var body = expr.Body.ToString();
        var index = body.IndexOf('.') + 1;
        return index <= 0 ? null : body[index..];
    }

    public static void UseParameter<TParameter>(this ParameterView view,
        string name, Action<TParameter> action)
    {
        if (view.TryGetValue<TParameter>(name, out var value))
            action(value);
    }

    public static RenderFragment GenerateSettingContent<TComponent>(this TComponent c)
        where TComponent : SComponentBase, IChildContentComponent => builder =>
    {
        builder.OpenComponent<CascadingValue<TComponent>>(0);
        builder.AddComponentParameter(1,
            nameof(CascadingValue<TComponent>.Value), c);
        builder.AddComponentParameter(2,
            nameof(CascadingValue<TComponent>.Name),
            nameof(SSettingComp<TComponent>.MasterComponent));
        builder.AddComponentParameter(3,
            nameof(CascadingValue<TComponent>.IsFixed), true);
        builder.AddComponentParameter(4,
            nameof(CascadingValue<TComponent>.ChildContent), c.ChildContent);
        builder.CloseComponent();
    };
}