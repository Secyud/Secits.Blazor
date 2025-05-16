using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class TimePickerClock : IValueComponent<TimeOnly>
{
    protected override string ComponentName => "tp-clock";

    [Parameter]
    public TimeOnly Value { get; set; }

    [Parameter]
    public EventCallback<TimeOnly> ValueChanged { get; set; }

}