using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Abstraction;

namespace Secyud.Secits.Blazor.Components;

public partial class STimePickerClock : ISchValue<TimeOnly>
{
    protected override string ComponentName => "tp-clock";

    [Parameter]
    public TimeOnly Value { get; set; }

    [Parameter]
    public EventCallback<TimeOnly> ValueChanged { get; set; }

}