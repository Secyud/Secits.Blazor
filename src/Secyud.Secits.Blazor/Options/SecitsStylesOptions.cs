namespace Secyud.Secits.Blazor.Options;

public class SecitsStylesOptions
{
    public const string RootPath = "_content/Secyud.Secits.Blazor/";

    public List<Func<SecitsThemeParam, IEnumerable<HtmlPathResource>>> Styles { get; } = [];

    public List<HtmlPathResource> Get(SecitsThemeParam? param = null)
    {
        param ??= new SecitsThemeParam();
        List<HtmlPathResource> res = [];
        switch (param.ThemeColor)
        {
            case UiThemeColor.Default:
                res.Add(new HtmlPathResource(RootPath + "css/color/default.min.css", "secits-theme-color"));
                break;
            case UiThemeColor.Unset:
            default:
                break;
        }

        switch (param.ThemeParam)
        {
            case UiThemeParam.Default:
                res.Add(new HtmlPathResource(RootPath + "css/param/default.min.css", "secits-theme-param"));
                break;
            case UiThemeParam.Unset:
            default:
                break;
        }
        switch (param.ThemeStyle)
        {
            case UiThemeStyle.Default:
                res.Add(new HtmlPathResource(RootPath + "css/style/default.min.css", "secits-theme-style"));
                break;
            case UiThemeStyle.Unset:
            default:
                break;
        }

        foreach (var style in Styles)
        {
            foreach (var str in style(param))
            {
                res.Add(str);
            }
        }

        return res;
    }
}