using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class EnableValueInput<TValue> : EnableInputDelayInvokerBase<TValue>, IHasCurrentValue<TValue>, IHasValue<TValue>
{
    public TValue CurrentValue
    {
        get => LastActiveItem;
        set => LastActiveItem = value;
    }

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    [Parameter]
    public Expression<Func<TValue>>? ValueExpression { get; set; }

    [Parameter]
    public Func<TValue, List<ValidationResult>>? ValueValidator { get; set; }

    protected override async Task OnValueChangedAsync()
    {
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(CurrentValue);
            await ChangeValidationAsync(CurrentValue, ValueExpression, ValueValidator);
        }
    }

    public override bool IsItemSelected(TValue value)
    {
        return Equals(CurrentValue, value);
    }

    protected override async Task OnClearActiveItemAsync()
    {
        CurrentValue = default!;
        await Task.CompletedTask;
    }

    protected override async Task OnSetActiveItemAsync(TValue value)
    {
        CurrentValue = value;
        await Task.CompletedTask;
    }
}