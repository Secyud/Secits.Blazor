using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TItem))]
public class EnableIteratorSelect<TItem> : SSelectorPluginBase
    <SIteratorBase<TItem>, SInput<TItem>, TItem>, IRowRenderer<TItem>
{
    protected IInputInvoker<TItem> Invoker => Selectable.InputInvoker.Get()!;
    
    protected override void ApplySelectable()
    {
    }

    protected override void ForgoSelectable()
    {
    }

    public override bool IsItemSelected(TItem value)
    {
        return Invoker.IsItemSelected(value);
    }

    public override async Task ClearActiveItemAsync()
    {
        await Invoker.ClearActiveItemAsync();
    }

    public override async Task SetActiveItemAsync(TItem value)
    {
        await Invoker.SetActiveItemAsync(value);
    }

    public override TItem GetActiveItem()
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

    public virtual string? GetRowClass(TItem item)
    {
        return IsItemSelected(item) ? "selected" : null;
    }

    public virtual string? GetRowStyle(TItem item)
    {
        return null;
    }

    public virtual void OnRowClick(MouseEventArgs args, TItem item)
    {
        SetActiveItemAsync(item).ConfigureAwait(false);
    }
}