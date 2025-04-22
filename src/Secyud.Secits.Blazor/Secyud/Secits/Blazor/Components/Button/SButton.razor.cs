using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Basic;
using Secyud.Secits.Blazor.Parameters;

namespace Secyud.Secits.Blazor.Components;

public partial class SButton : IClickComponent,IColorComponent
{
    protected override string ComponentName => "btn";
    protected override string ElementName => "button";

    protected override void BuildInitialClassStyle(ClassStyleBuilderContext context)
    {
        context.AppendClass("bd");
    }

    [Parameter]
    public EventCallback Click { get; set; }

    [Parameter]
    public ColorType Color { get; set; }
}