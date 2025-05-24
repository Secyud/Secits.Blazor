using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public partial class SCard
{
    protected override string ComponentName => "card";

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public string? Text { get; set; }
}