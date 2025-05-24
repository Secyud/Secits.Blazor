namespace Secyud.Secits.Blazor.Components;

public interface IInputMask
{
    public bool TryParseMask(string? origin, out string? target);
    public bool TryConvertMaskToText(string? mask, out string? origin);
    public bool TryConvertTextToMask(string? mask, out string? origin);
}