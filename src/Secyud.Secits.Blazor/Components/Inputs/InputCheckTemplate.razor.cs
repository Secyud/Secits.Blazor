using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class InputCheckTemplate
{
    [Parameter]
    public bool SubmitOnInput { get; set; }

    protected bool CachedValue { get; set; }

    public override RendererPosition GetLayoutPosition()
    {
        return RendererPosition.Body;
    }

    protected bool GetValue()
    {
        if (Master.InputInvoker.Get() is { } invoker)
        {
            return invoker.GetActiveItem();
        }

        return false;
    }

    protected async Task OnClickAsync()
    {
        await SetValueAsync(!GetValue());
    }

    protected async Task SetValueAsync(bool value)
    {
        if (Master.InputInvoker.Get() is { } invoker)
        {
            await invoker.SetActiveItemAsync(this, value);
        }
    }
}