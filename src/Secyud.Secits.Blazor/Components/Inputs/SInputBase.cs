using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Components;

public abstract class SInputBase<TValue> : ScBusinessBase, ISchValue<TValue>, IScsTheme, IScsActive
{
    #region Parameters

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    [Parameter]
    public Theme Theme { get; set; }

    [Parameter]
    public Size Size { get; set; }

    [Parameter]
    public Style StyleOption { get; set; }

    [Parameter]
    public bool Readonly { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    #endregion

    public TValue CurrentValue { get; protected set; } = default!;

    public virtual async Task OnValueChangedAsync(TValue value)
    {
        CurrentValue = value;
        await ValueChanged.InvokeAsync(value);
    }
}