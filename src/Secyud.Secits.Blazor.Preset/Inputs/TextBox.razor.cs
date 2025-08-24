using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Preset;

[CascadingTypeParameter(nameof(TValue))]
public partial class TextBox<TValue>
    where TValue :
    IComparable<string?>,
    IEquatable<string?>,
    ISpanParsable<string>,
    IParsable<string>
{
    [Parameter]
    public bool SubmitOnInput { get; set; }
}