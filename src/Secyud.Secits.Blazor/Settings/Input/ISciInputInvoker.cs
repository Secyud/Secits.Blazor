namespace Secyud.Secits.Blazor;

public interface ISciInputInvoker<in TValue>
{
    Task InvokeValueChanged(object? sender, TValue value);
}