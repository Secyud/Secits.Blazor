using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Validations;

namespace Secyud.Secits.Blazor;

public partial class SValidationMessage : IValidationListener
{
    [CascadingParameter]
    public ValidationField? ValidationField { get; set; }

    public async Task OnValidationChangedAsync()
    {
        await InvokeAsync(StateHasChanged);
    }
}