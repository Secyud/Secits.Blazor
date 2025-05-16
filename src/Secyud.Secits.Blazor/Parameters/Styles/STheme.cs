namespace Secyud.Secits.Blazor;

[Flags]
public enum STheme
{
    Color = 0b111,
    Style = 0b11 << 4,
    Size = 0b111 << 6,
    Shadow = 1 << 9,
    Angular = 1 << 10,
    Rounded = 1 << 11,

    Primary = 0,
    Default = Primary,
    Secondary = 0b001,
    Success = 0b010,
    Info = 0b011,
    Warning = 0b100,
    Danger = 0b101,
    Light = 0b110,
    Dark = Color,

    Outlined = 0b01 << 4,
    NoBorder = 0b10 << 4,
    
    XSmall = 0b001 << 6,
    Small = 0b010 << 6,
    Large = 0b011 << 6,
    XLarge = 0b100 << 6,
}