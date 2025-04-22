namespace Secyud.Secits.Blazor.Components;

public class SInputText : SInputBase<string?>
{
    protected override bool TryConvertToValue(string? value, out string? output)
    {
        output = value;
        return true;
    }
}