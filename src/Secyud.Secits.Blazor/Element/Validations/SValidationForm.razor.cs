using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Element;

public partial class SValidationForm : IHasContent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    public List<Validation> Validations { get; } = [];
}