namespace Secyud.Secits.Blazor.Services;

public class NullLocalizationService : ILocalizationService
{
    public string Localize(string str)
    {
        return str;
    }
}