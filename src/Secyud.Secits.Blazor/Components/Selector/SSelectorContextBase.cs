namespace Secyud.Secits.Blazor;

public class SSelectorContextBase<TSelection>
{
    public SSelectorContextBase(ISchTextField<TSelection> textField)
    {
        TextField = textField;
    }

    public ISchTextField<TSelection> TextField { get; }
}