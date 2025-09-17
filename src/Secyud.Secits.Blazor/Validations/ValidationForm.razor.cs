using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Validations;

public partial class ValidationForm : IHasContent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    public SSettings<ValidationField> Fields { get; } = new();
}