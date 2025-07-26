namespace Secyud.Secits.Blazor;

public interface IHasCustomCss
{
    string? Class { get; set; }

    string? Style { get; set; }
}