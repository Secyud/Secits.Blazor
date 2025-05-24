namespace Secyud.Secits.Blazor.JSInterop;

public interface IJsWindowService
{
    public ValueTask CancelEvent(string eventName);

    public ValueTask RestoreEvent(string eventName);
}