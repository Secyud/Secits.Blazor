namespace Secyud.Secits.Blazor.Parameters;

public interface IValueDelegateComponent<TValue> : IValueComponent<TValue>
{
    Action<TValue?>? OnValueParameterSet { get; set; }
}