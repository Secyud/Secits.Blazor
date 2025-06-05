namespace Secyud.Secits.Blazor.Components;

public interface ISciInputInvoker<in TValue>
{
    Task InvokeValueChanged(object? sender, TValue value);
}