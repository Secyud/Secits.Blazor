using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public abstract class SValueMultiSelectorBase<TComponent, TValue> :
    SSelectorBase<TComponent, TValue>, ISchValues<TValue>
    where TComponent : ScBusinessBase
{
    [Parameter]
    public IEnumerable<TValue> Values { get; set; } = [];

    [Parameter]
    public EventCallback<IEnumerable<TValue>> ValuesChanged { get; set; }

    protected virtual async Task OnValuesSelectChangedAsync(IEnumerable<TValue> values)
    {
        values = values.ToList();
        Values = values;
        if (ValuesChanged.HasDelegate)
            await ValuesChanged.InvokeAsync(values);
    }

    protected override bool IsSelected(TValue? selection)
    {
        return selection is not null && Values.Contains(selection);
    }

    public override async Task ClearSelectAsync()
    {
        await OnValuesSelectChangedAsync([]);
    }
}