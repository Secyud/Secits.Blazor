namespace Secyud.Secits.Blazor.PageRouter;

[AttributeUsage(AttributeTargets.Class)]
public class PageRouterAttribute : Attribute
{
    public string? Name { get; set; }
    public Type? ResourceType { get; set; }
    public string[] Parameters { get; set; } = [];
    public string[] ParameterPrefixes { get; set; } = [];
}