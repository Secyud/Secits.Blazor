using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class InputSliderBoxTemplate : IHasRange<int>, ILayoutTemplateRenderer
{
    [Parameter, Range(1, 4)]
    public int NumberCount { get; set; } = 1;

    [Parameter]
    public int Max { get; set; } = 100;

    [Parameter]
    public int Min { get; set; }

    [Parameter]
    public bool Cycle { get; set; }

    [Parameter]
    public string? Format { get; set; }
}