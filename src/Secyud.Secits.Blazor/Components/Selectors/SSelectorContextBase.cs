namespace Secyud.Secits.Blazor.Components;

public class SSelectorContextBase<TSelection>
{
    public SSelectorContextBase(ISchTextField<TSelection> textField)
    {
        TextField = textField;
    }

    public ISchTextField<TSelection> TextField { get; }
}