using Secyud.Secits.Blazor.Icons;

namespace Secyud.Secits.Blazor;

public class FontAwesomeIconProvider : IIconProvider
{
    private readonly Dictionary<IconName, string> _fontAwesomeIcons = new Dictionary<IconName, string>()
    {
        [IconName.Create] = "fas fa-file",
        [IconName.Update] = "fas fa-pen-to-square",
        [IconName.Delete] = "fas fa-trash",
        [IconName.Search] = "fas fa-magnifying-glass",
        [IconName.Clear] = "fas fa-circle-xmark",
        [IconName.DropDown] = "fas fa-caret-down",
        [IconName.FirstPage] = "fas fa-angles-left",
        [IconName.PreviewPage] = "fas fa-angle-left",
        [IconName.NextPage] = "fas fa-angle-right",
        [IconName.LastPage] = "fas fa-angles-right",
    };

    public string GetIcon(IconName name)
    {
        return _fontAwesomeIcons[name];
    }
}