using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract class EnableSelectBase<TComponent, TSelection> : SSettingBase<TComponent>,
    IHasSelectContent, IHasTextField<TSelection> where TComponent : SComponentBase
{
    private ISelectionDisplayer? _selectDelegate;

    [CascadingParameter]
    public ISelectionDisplayer? SelectDelegate
    {
        get => _selectDelegate;
        set
        {
            _selectDelegate?.Selector.Forgo(this);
            _selectDelegate = value;
            _selectDelegate?.Selector.Apply(this);
        }
    }

    [Parameter]
    public Func<TSelection, string?>? TextField { get; set; }

    protected virtual string? GetText(TSelection? selection)
    {
        return TextField is null || selection is null ? selection?.ToString() : TextField(selection);
    }

    public abstract RenderFragment? GenerateSelectedContent();
    public abstract Task ClearSelectAsync();
    public abstract bool IsSelected(TSelection? selection);
    public abstract Task OnSelectionActivateAsync(TSelection selection);
}