namespace Secyud.Secits.Blazor.Components;

public partial class SInputClearButton<TValue> : ISciInputSlotRenderer<TValue>
{
    private async Task ClearInputAsync()
    {
        if (Master is not null)
        {
            await Master.OnValueChangedAsync(default!);
        }
    }

    public Task SetValueFromParameterAsync(TValue value)
    {
        return Task.CompletedTask;
    }
}