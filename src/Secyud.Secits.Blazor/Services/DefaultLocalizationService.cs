namespace Secyud.Secits.Blazor.Services;

public class DefaultLocalizationService:ILocalizationService
{
    public string Localize(string str)
    {
        return str;
    }
}