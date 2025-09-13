using System.ComponentModel.DataAnnotations;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class Validation
{
    public List<ValidationResult>? Results { get; private set; }
    public SSettings<IValidationListener> Listeners { get; } = new();

    public async Task OnValidationChangedAsync(List<ValidationResult>? results)
    {
        Results = results;
        await Listeners.InvokeAsync(OnValidationChangedAsync);
    }

    private async Task OnValidationChangedAsync(IValidationListener listener)
    {
        await listener.OnValidationChangedAsync();
    }
}