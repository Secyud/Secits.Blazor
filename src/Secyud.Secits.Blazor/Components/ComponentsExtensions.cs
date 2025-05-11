using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public static class ComponentsExtensions
{
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