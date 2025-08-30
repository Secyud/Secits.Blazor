using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public class EnableIteratorSelect<TValue> : SSelectorPluginBase
    <SIteratorBase<TValue>, SInput<TValue>, TValue>, IRowRenderer<TValue>
{
    protected IInputInvoker<TValue> Invoker => Selectable.InputInvoker.Get()!;

    public override bool IsItemSelected(TValue value)
    {
        return SelectableValid && Invoker.IsItemSelected(value);
    }

    public override async Task ClearActiveItemAsync()
    {
        await Invoker.ClearActiveItemAsync(this);
    }

    public override async Task SetActiveItemAsync(TValue value)
    {
        await Invoker.SetActiveItemAsync(this, value);
    }

    public override TValue GetActiveItem()
    {
        return Invoker.GetActiveItem();
    }

    protected override void ApplySetting()
    {
        Master.RowRenderer.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.RowRenderer.Forgo(this);
    }

    public virtual string? GetRowClass(TValue item)
    {
        return IsItemSelected(item) ? "selected" : null;
    }

    public virtual string? GetRowStyle(TValue item)
    {
        return null;
    }

    public virtual void OnRowClick(MouseEventArgs args, TValue item)
    {
        SetActiveItemAsync(item).ConfigureAwait(false);
    }
}