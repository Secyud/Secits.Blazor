using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public abstract class SElementComp : SComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override RenderFragment? GenerateChildContent()
    {
        return ChildContent;
    }
}