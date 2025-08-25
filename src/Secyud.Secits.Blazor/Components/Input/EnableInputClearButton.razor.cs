using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Icons;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableInputClearButton<TValue> : ILayoutTemplateRenderer
{
    [Inject]
    private IIconProvider IconProvider { get; set; } = null!;

    private async Task ClearInputAsync(MouseEventArgs args)
    {
        if (Master.InputInvoker.Get() is { } invoker)
            await invoker.ClearActiveItemAsync(this);
    }
}