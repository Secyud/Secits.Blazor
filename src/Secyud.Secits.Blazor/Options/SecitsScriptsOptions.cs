namespace Secyud.Secits.Blazor.Options;

public class SecitsScriptsOptions
{
    public const string RootPath = "_content/Secyud.Secits.Blazor/";

    public List<Func<SecitsThemeParam, IEnumerable<HtmlPathResource>>> Scripts { get; } = [];

    public List<HtmlPathResource> Get(SecitsThemeParam? param = null)
    {
        param ??= new SecitsThemeParam();
        List<HtmlPathResource> res = [..GetStaticScripts()];

        foreach (var script in Scripts)
        {
            foreach (var str in script(param))
            {
                res.Add(str);
            }
        }

        return res;
    }

    public static List<HtmlPathResource> GetStaticScripts()
    {
        return
        [
            new HtmlPathResource(RootPath + "js/components.js", "secits-component-js"),
        ];
    }
}