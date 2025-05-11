namespace Secyud.Secits.Blazor;

public interface IValueDelegateComponent<TValue> : IValueComponent<TValue>
{
    Action<TValue>? OnValueParameterSet { get; set; }
}