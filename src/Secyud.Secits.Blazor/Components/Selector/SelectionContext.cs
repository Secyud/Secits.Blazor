using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class SelectionContext<TSelection> : SelectionContextBase<TSelection>
{
    public SelectionContext(IHasTextField<TSelection> textField, TSelection? selection) : base(textField)
    {
        Selection = selection;
    }

    public TSelection? Selection { get; }
}