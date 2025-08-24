using Secyud.Secits.Blazor.Icons;

namespace Secyud.Secits.Blazor;

public class FontAwesomeIconProvider : IIconProvider
{
    private readonly Dictionary<IconType, string> _fontAwesomeIcons = new Dictionary<IconType, string>()
    {
        [IconType.Create] = "fas fa-file",
        [IconType.Update] = "fas fa-pen-to-square",
        [IconType.Delete] = "fas fa-trash",
        [IconType.Search] = "fas fa-magnifying-glass",
        [IconType.Clear] = "fas fa-circle-xmark",
        [IconType.DropDown] = "fas fa-caret-down",
    };

    public string GetIcon(IconType type)
    {
        return _fontAwesomeIcons[type];
    }
}