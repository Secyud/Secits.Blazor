using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SCard
{
    protected override string ComponentName => "card";

    [Parameter]
    public RenderFragment? Header { get; set; }

    [Parameter]
    public RenderFragment? Body { get; set; }
}