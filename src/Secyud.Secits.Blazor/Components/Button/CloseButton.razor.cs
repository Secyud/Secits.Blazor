using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Abstraction;

namespace Secyud.Secits.Blazor.Components;

public partial class CloseButton : ISccClick
{
    protected override string ComponentName => "button-close";
    protected override string ElementName => "span";

    [Parameter]
    public EventCallback Click { get; set; }

    protected override int BuildContentExtra(RenderTreeBuilder builder, int sequence)
    {
        builder.AddAttribute(sequence + 1, "onclick", Click);
        return sequence + 1;
    }

    protected override string GetClass()
    {
        return ComponentName;
    }
}