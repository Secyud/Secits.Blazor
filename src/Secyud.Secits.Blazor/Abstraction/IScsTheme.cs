using Secyud.Secits.Blazor.Arguments;

namespace Secyud.Secits.Blazor.Abstraction;

public interface IScsTheme
{
    Theme Theme { get; set; }
    Size Size { get; set; }
    Style StyleOption { get; set; }
}