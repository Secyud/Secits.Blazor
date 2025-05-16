namespace Secyud.Secits.Blazor;

public interface IThemeComponent
{
    Theme Theme { get; set; }
    Size Size { get; set; }

    bool Borderless { get; set; }
    bool Shadow { get; set; }
    bool Background { get; set; }
    bool Angular { get; set; }
    bool Rounded { get; set; }
}