using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public abstract partial class ScBusinessBase
{
    private readonly ScSettingMaster _settingMaster;

    protected ScBusinessBase()
    {
        _settingMaster = new ScSettingMaster(this);
    }

    internal void MasterStateHasChanged()
    {
        StateHasChanged();
    }

    internal Task MasterInvokeAsync(Action action)
    {
        return InvokeAsync(action);
    }

    internal Task MasterInvokeAsync(Func<Task> action)
    {
        return InvokeAsync(action);
    }
}