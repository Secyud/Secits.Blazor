using Secyud.Secits.Blazor.Icons;

namespace Secyud.Secits.Blazor;

public class FontAwesomeIconProvider : IIconProvider
{
    private readonly Dictionary<IconName, string> _fontAwesomeIcons = new()
    {
        [IconName.Create] = "fas fa-file",
        [IconName.Update] = "fas fa-pen-to-square",
        [IconName.Delete] = "fas fa-trash",
        [IconName.Search] = "fas fa-magnifying-glass",
        [IconName.Cross] = "fas fa-circle-xmark",
        [IconName.DropDown] = "fas fa-caret-down",
        [IconName.LeftAngles] = "fas fa-angles-left",
        [IconName.LeftAngle] = "fas fa-angle-left",
        [IconName.RightAngle] = "fas fa-angle-right",
        [IconName.RightAngles] = "fas fa-angles-right",
        [IconName.UpAngle] = "fas fa-angle-up",
        [IconName.DownAngle] = "fas fa-angle-down",
        [IconName.UpAngles] = "fas fa-angles-up",
        [IconName.DownAngles] = "fas fa-angles-down",
        [IconName.Pin] = "fas fa-thumbtack",
        [IconName.Bars] = "fas fa-bars",
    };

    public string GetIcon(IconName name)
    {
        return _fontAwesomeIcons[name];
    }
}