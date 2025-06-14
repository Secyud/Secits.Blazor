using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class SelectionContextBase<TSelection>
{
    public SelectionContextBase(IHasTextField<TSelection> textField)
    {
        TextField = textField;
    }

    public IHasTextField<TSelection> TextField { get; }
}