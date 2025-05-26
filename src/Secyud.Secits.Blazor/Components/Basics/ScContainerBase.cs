using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

/// <summary>
/// Represents an abstract base class for container components in the Secyud.Secits.Blazor namespace.
/// This class inherits from ScBusinessBase and implements the IChildContentComponent interface,
/// providing a foundation for components that support child content rendering.
/// It includes a parameter for defining child content and overrides the mechanism for generating
/// child content based on the provided RenderFragment.
/// </summary>
public abstract class ScContainerBase : ScStyledBase, ISchContent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override RenderFragment? GenerateChildContent()
    {
        return ChildContent;
    }
}