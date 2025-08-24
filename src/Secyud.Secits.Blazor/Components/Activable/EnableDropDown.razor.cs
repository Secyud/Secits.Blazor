using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Icons;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableDropDown : IHasContent
{
    private bool _isDropDownVisible;

    [Inject]
    private IIconProvider IconProvider { get; set; } = null!;

    [Parameter]
    public string? ContentClass { get; set; }

    [Parameter]
    public string? ContentStyle { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}