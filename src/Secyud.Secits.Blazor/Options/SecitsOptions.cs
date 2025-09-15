namespace Secyud.Secits.Blazor.Options;

public class SecitsOptions
{
    public const string RootPath = "_content/Secyud.Secits.Blazor/";
    public const string CookieColor = "secits-color";
    public const string CookieStyle = "secits-style";
    public const string CookieParam = "secits-param";

    public UiThemeColor ThemeColor { get; set; } = UiThemeColor.Default;
    public UiThemeStyle ThemeStyle { get; set; } = UiThemeStyle.Default;
    public UiThemeParam ThemeParam { get; set; } = UiThemeParam.Default;

    public List<string> CssPaths { get; } = [];
    public List<string> JsPaths { get; } = [];

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

        res.AddRange(CssPaths);

        return res;
    }

    public List<string> GetJsPaths()
    {
        List<string> res =
        [
            RootPath + "js/components.js",
            RootPath + "js/event-handler.js",
            ..JsPaths
        ];

        return res;
    }

    public void MapFrom(SecitsOptions options)
    {
        ThemeColor = options.ThemeColor;
        ThemeParam = options.ThemeParam;
        ThemeStyle = options.ThemeStyle;
        CssPaths.Clear();
        CssPaths.AddRange(options.CssPaths);
        JsPaths.Clear();
        JsPaths.AddRange(options.JsPaths);
        Parameters.Clear();
        Parameters.AddRange(options.Parameters);
    }
}