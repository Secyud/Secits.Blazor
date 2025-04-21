namespace Secyud.Secits.Blazor.Parameters;

public interface IValueDelegateComponent<TValue> : IValueComponent<TValue>
{
    Action<TValue?>? ValueSetAction { get; set; }
}