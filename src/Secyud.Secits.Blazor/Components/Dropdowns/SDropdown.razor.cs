namespace Secyud.Secits.Blazor;

public partial class SDropdown : ITextDelegateComponent
{
    protected override string ComponentName => "dropdown";

    public event Action<string?>? TextChangedEvent;

    protected override void OnValueChanged(string? value)
    {
        base.OnValueChanged(value);
        TextChangedEvent?.Invoke(value);
    }
}