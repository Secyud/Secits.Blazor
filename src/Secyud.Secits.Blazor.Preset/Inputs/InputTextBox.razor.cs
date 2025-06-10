using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Preset;

public partial class InputTextBox
{
    [Parameter]
    public bool SubmitOnInput { get; set; }
}