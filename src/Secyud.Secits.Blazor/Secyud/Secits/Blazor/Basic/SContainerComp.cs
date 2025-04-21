using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Parameters;

namespace Secyud.Secits.Blazor.Basic;

public abstract class SContainerComp : SBasicComp, IChildContentComponent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override RenderFragment? GenerateChildContent()
    {
        return ChildContent;
    }
}