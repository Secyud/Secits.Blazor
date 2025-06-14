namespace Secyud.Secits.Blazor;

public partial class SInputClearButton<TValue> : ISciValueContainer<TValue>
{
    private async Task ClearInputAsync()
    {
        await Master.OnValueChangedAsync(default!);
    }

    protected override void ApplySetting()
    {
        Master.ValueContainer.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.ValueContainer.Forgo(this);
    }
}