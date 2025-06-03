namespace Secyud.Secits.Blazor.Options;

public class SecitsOptions
{
    public const string RootPath = "_content/Secyud.Secits.Blazor/";

    public UiThemeType ThemeType { get; set; } = UiThemeType.Default;


    public List<string> GetCssPaths()
    {
        List<string> res = [];

        switch (ThemeType)
        {
            case UiThemeType.Default:
                res.Add(RootPath + "css/style/default.css");
                break;
            case UiThemeType.BootstrapV5:
                res.Add(RootPath + "css/style/bootstrap-v5.css");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        res.Add(RootPath + "css/components.css");

        return res;
    }

    public List<string> GetJsPaths()
    {
        List<string> res =
        [
            RootPath + "js/components.js",
            RootPath + "js/event-handler.js",
        ];

        return res;
    }
}