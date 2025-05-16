namespace Secyud.Secits.Blazor;

public interface ITextDelegateComponent
{
    public event Action<string?>? TextChangedEvent;
}