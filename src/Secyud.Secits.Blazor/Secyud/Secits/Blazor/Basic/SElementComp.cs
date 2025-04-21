using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor.Basic;

public abstract class SElementComp : SComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override RenderFragment? GenerateChildContent()
    {
        return ChildContent;
    }
}