using Secyud.Secits.Blazor.Icons;

namespace Secyud.Secits.Blazor;

public class FontAwesomeIconProvider : IIconProvider
{
    public string GetIcon(IconType type)
    {
        return type switch
        {
            IconType.Create => "fas fa-file",
            IconType.Update => "fas fa-pen-to-square",
            IconType.Delete => "fas fa-trash",
            IconType.Search => "fas fa-magnifying-glass",
            IconType.Clear => "fas fa-circle-xmark",
            _ => ""
        };
    }
}