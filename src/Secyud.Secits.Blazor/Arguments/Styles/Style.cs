namespace Secyud.Secits.Blazor;

[Flags]
public enum Style : byte
{
    Borderless = 1 << 0,
    Shadow = 1 << 1,
    Background = 1 << 2,
    Angular = 1 << 3,
    Rounded = 1 << 4,
    Plain = 1 << 5,
}