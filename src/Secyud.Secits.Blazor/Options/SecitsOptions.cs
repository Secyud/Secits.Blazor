namespace Secyud.Secits.Blazor.Options;

public class SecitsOptions
{
    public const string RootPath = "_content/Secyud.Secits.Blazor/";

    public UiThemeColor ThemeColor { get; set; } = UiThemeColor.Default;
    public UiThemeStyle ThemeStyle { get; set; } = UiThemeStyle.Default;
    public UiThemeParam ThemeParam { get; set; } = UiThemeParam.Default;

    public List<DirtyParameter> Parameters { get; } = [];

    public List<string> GetCssPaths()
    {
        List<string> res = [];

        res.AddRange(ThemeColor switch
        {
            UiThemeColor.Default => [RootPath + "css/color/default.min.css"],
            _ => throw new ArgumentOutOfRangeException(),
        });
        
        res.AddRange(ThemeParam switch
        {
            UiThemeParam.Default => [RootPath + "css/param/default.min.css"],
            _ => throw new ArgumentOutOfRangeException(),
        });
        
        res.AddRange(ThemeStyle switch
        {
            UiThemeStyle.Default => [RootPath + "css/style/default.min.css"],
            _ => throw new ArgumentOutOfRangeException(),
        });

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