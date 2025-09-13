namespace Secyud.Secits.Blazor;

public interface IValidationListener
{
    Task OnValidationChangedAsync();
}