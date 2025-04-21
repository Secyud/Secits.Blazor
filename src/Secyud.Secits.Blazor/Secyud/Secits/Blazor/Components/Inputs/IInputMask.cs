namespace Secyud.Secits.Blazor.Components;

public interface IInputMask
{
    public bool TryMaskValue(string? origin, out string? target);
    public bool TryUnmaskValue(string? origin, out string? target);
}