namespace Secyud.Secits.Blazor.Components;

public class SMultiSelectorContext<TSelection> : SSelectorContextBase<TSelection>
{
    public SMultiSelectorContext(ISchTextField<TSelection> textField, IEnumerable<TSelection> selections) :
        base(textField)
    {
        Selections = selections;
    }

    public IEnumerable<TSelection> Selections { get; }
}