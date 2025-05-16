using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SButton : IClickComponent, IThemeComponent
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
    public bool Borderless { get; set; }

    [Parameter]
    public bool Shadow { get; set; }

    [Parameter]
    public bool Background { get; set; }

    [Parameter]
    public bool Angular { get; set; }

    [Parameter]
    public bool Rounded { get; set; }
}