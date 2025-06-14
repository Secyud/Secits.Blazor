using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract class EnableMultiValueSelectBase<TComponent, TValue> : EnableSelectBase<TComponent, TValue>,
    IHasValues<TValue>
    where TComponent : SComponentBase
{
    [Parameter]
    public List<TValue> Values { get; set; } = [];

    [Parameter]
    public EventCallback<List<TValue>> ValuesChanged { get; set; }

    [Parameter]
    public RenderFragment<SelectionsContext<TValue>>? SelectContent { get; set; }

    public override RenderFragment? GenerateSelectedContent() =>
        SelectContent?.Invoke(new(this, Values));

    protected virtual async Task OnValuesSelectChangedAsync(List<TValue> values)
    {
        values = values.ToList();
        Values = values;
        if (ValuesChanged.HasDelegate)
            await ValuesChanged.InvokeAsync(values);
    }

    public override bool IsSelected(TValue? selection)
    {
        return selection is not null && Values.Contains(selection);
    }

    public override async Task ClearSelectAsync()
    {
        await OnValuesSelectChangedAsync([]);
    }

    public override async Task OnSelectionActivateAsync(TValue selection)
    {
        var list = Values.ToList();
        if (!list.Remove(selection)) list.Add(selection);
        await OnValuesSelectChangedAsync(list);
    }
}