using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Element;

public class Validation : IAsyncDisposable
{
    private SValidationForm? _form;
    public bool IsValid { get; private set; }
    public List<ValidationResult>? Results { get; private set; }
    public SSettings<IValidationListener> Listeners { get; } = new();

    [CascadingParameter]
    public SValidationForm? Form
    {
        get => _form;
        set
        {
            _form?.Validations.Remove(this);
            _form = value;
            _form?.Validations.Add(this);
        }
    }

    public async Task OnValidationChangedAsync(List<ValidationResult>? results, bool isValid)
    {
        Results = results;
        IsValid = isValid;
        await Listeners.InvokeAsync(OnValidationChangedAsync);
    }

    private async Task OnValidationChangedAsync(IValidationListener listener)
    {
        await listener.OnValidationChangedAsync();
    }

    public virtual ValueTask DisposeAsync()
    {
        Form = null;
        return ValueTask.CompletedTask;
    }
}