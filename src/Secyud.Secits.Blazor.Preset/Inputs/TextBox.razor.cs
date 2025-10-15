using System.Collections;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Preset;

[CascadingTypeParameter(nameof(TValue))]
public partial class TextBox<TValue> : IHasValue<TValue>
    where TValue :
    IComparable,
    IEnumerable,
    IConvertible,
    IEnumerable<char>,
    IComparable<string?>,
    IEquatable<string?>,
    ICloneable,
    ISpanParsable<string>,
    IParsable<string>
{
    [Parameter]
    public bool SubmitOnInput { get; set; }

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }
}