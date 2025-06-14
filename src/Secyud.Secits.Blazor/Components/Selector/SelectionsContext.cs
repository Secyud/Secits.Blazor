using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class SelectionsContext<TSelection> : SelectionContextBase<TSelection>
{
    public SelectionsContext(IHasTextField<TSelection> textField, IEnumerable<TSelection> selections) :
        base(textField)
    {
        Selections = selections;
    }

    public IEnumerable<TSelection> Selections { get; }
}