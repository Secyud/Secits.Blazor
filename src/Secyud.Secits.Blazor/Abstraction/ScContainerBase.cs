using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Abstraction;

/// <summary>
/// Represents an abstract base class for container components in the Secyud.Secits.Blazor namespace.
/// This class inherits from ScBusinessBase and implements the IChildContentComponent interface,
/// providing a foundation for components that support child content rendering.
/// It includes a parameter for defining child content and overrides the mechanism for generating
/// child content based on the provided RenderFragment.
/// </summary>
public abstract class ScContainerBase : ScBusinessBase, ISchContent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override RenderFragment? GenerateChildContent()
    {
        return ChildContent;
    }

    public RenderFragment GenerateSettingContent<TComponent>()
        where TComponent : class => builder =>
    {
        builder.OpenComponent<CascadingValue<TComponent>>(0);
        builder.AddComponentParameter(1,
            nameof(CascadingValue<TComponent>.Value), this);
        builder.AddComponentParameter(2,
            nameof(CascadingValue<TComponent>.Name),
            nameof(ScSettingBase<TComponent>.MasterComponent));
        builder.AddComponentParameter(3,
            nameof(CascadingValue<TComponent>.IsFixed), true);
        builder.AddComponentParameter(4,
            nameof(CascadingValue<TComponent>.ChildContent), ChildContent);
        builder.CloseComponent();
    };
}