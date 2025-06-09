using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public abstract class SSelectorBase<TComponent, TSelection> : ScSettingBase<TComponent>,
    ISciSelect, ISchTextField<TSelection> where TComponent : ScBusinessBase
{
    private IScdSelect? _selectDelegate;

    [CascadingParameter]
    public IScdSelect? SelectDelegate
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

    public abstract RenderFragment GenerateSelectedContent();
    public abstract Task ClearSelectAsync();
    public abstract bool IsSelected(TSelection? selection);
    public abstract Task OnSelectionActivateAsync(TSelection selection);
}