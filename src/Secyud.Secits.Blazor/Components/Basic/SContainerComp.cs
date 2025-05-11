using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public abstract class SContainerComp : SBasicComp, IChildContentComponent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override RenderFragment? GenerateChildContent()
    {
        return ChildContent;
    }
}