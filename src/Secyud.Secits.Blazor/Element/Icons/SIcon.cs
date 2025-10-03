using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Element;

public class SIcon : SIconBase
{
    [Parameter]
    public string? IconName { get; set; }

    protected override string? Icon => IconName;
}