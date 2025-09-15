using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Element;

public partial class SProgressBar : IHasCustomCss
{
    [Parameter]
    public int Percentage { get; set; }

    public string? Class { get; set; }
    public string? Style { get; set; }

    protected string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("progress-bar", Class);
    }

    protected string GetStyle()
    {
        return $"--percentage:{Percentage};{Style}";
    }
}