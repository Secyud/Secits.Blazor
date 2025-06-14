namespace Secyud.Secits.Blazor;

public class SSingleSelectorContext<TSelection> : SSelectorContextBase<TSelection>
{
    public SSingleSelectorContext(ISchTextField<TSelection> textField, TSelection? selection) : base(textField)
    {
        Selection = selection;
    }

    public TSelection? Selection { get; }
}