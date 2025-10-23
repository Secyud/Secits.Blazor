using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Preset;

public partial class DropdownBox
{
    [Parameter]
    public RenderFragment? DropdownContentTemplate { get; set; }

    private EnableDropDown? _enableDropDown;

    protected override void OnClick()
    {
        base.OnClick();
        ClickDropDownAsync();
    }

    protected Task CloseDropDownAsync()
    {
        return _enableDropDown?.CloseDropDownAsync() ?? Task.CompletedTask;
    }

    protected Task ClickDropDownAsync()
    {
        return _enableDropDown?.ClickDropDownAsync() ?? Task.CompletedTask;
    }
}