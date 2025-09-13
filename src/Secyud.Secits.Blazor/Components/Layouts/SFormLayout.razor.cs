using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class SFormLayout
{
    protected override string ComponentName => "form-layout";

    [Parameter]
    public string? Gap { get; set; } = "1rem";

    [Parameter]
    public int? TitleWidth { get; set; }

    [Parameter]
    public bool EnableLabel { get; set; }

    protected override void BuildClassStyle(ClassStyleContext context)
    {
        base.BuildClassStyle(context);
        context.AppendStyle("grid-gap", Gap);
        context.AppendStyle("--label-width", TitleWidth?.ToString());
    }
}