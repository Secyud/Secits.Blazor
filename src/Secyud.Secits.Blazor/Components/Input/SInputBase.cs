using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract class SInputBase<TValue> : SActivableBase
{
    protected override string ComponentName => "input";
    public abstract Task SetSingleValue(TValue value);
    public abstract TValue GetSingleValue();
    public abstract Task OnValueCleared();

    #region Settings

    public SSettings<IValueContainer<TValue>> ValueContainer { get; } = new();
    public SSetting<IInputValueConverter<TValue>> ValueConverter { get; } = new();

    #endregion
}