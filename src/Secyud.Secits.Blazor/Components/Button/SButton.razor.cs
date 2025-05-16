using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SButton : IClickComponent,IThemeComponent
{
    protected override string ComponentName => "button";
    protected override string ElementName => "button";

    [Parameter]
    public EventCallback Click { get; set; }

    [Parameter]
    public STheme Theme { get; set; }
}