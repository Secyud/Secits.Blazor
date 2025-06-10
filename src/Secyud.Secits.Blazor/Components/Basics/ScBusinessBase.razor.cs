using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public abstract partial class ScBusinessBase
{
    private readonly ScSettingMaster _settingMaster;

    protected ScBusinessBase()
    {
        _settingMaster = new ScSettingMaster(this);
    }

    public void RefreshUi()
    {
        StateHasChanged();
    }

    public Task RefreshUiAsync()
    {
        return InvokeAsync(StateHasChanged);
    }
}