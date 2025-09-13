namespace Secyud.Secits.Blazor.Services;

public class NullSecitsLocalizationService : ISecitsLocalizationService
{
    public string Localize(string str)
    {
        return str;
    }
}