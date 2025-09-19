namespace Secyud.Secits.Blazor.Options;

public struct HtmlPathResource(string path, string id)
{
    public string Path { get; set; } = path;
    public string Id { get; set; } = id;
}