using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Abstraction;
using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

public partial class SButton : ISccClick, IScsTheme
{
    protected override string ComponentName => "button";
    protected override string ElementName => "button";

    [Parameter]
    public EventCallback Click { get; set; }

    [Parameter]
    public Theme Theme { get; set; }

    [Parameter]
    public Size Size { get; set; }

    [Parameter]
    public Style StyleOption { get; set; }

    protected override int BuildContentExtra(RenderTreeBuilder builder, int sequence)
    {
        builder.AddAttribute(sequence + 1, "onclick", Click);
        return sequence + 1;
    }
}