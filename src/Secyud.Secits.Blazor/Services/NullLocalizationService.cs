namespace Secyud.Secits.Blazor;

public class NullLocalizationService : ILocalizationService
{
    public string Localize(string str)
    {
        return str;
    }
}