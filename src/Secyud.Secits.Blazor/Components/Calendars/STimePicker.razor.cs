namespace Secyud.Secits.Blazor.Components;

public partial class STimePicker
{
    protected override string ComponentName => "pkr-t panel";

    private void OnInputInvoke(TimeOnly? timeOnly)
    {
        OnInputInvoke(timeOnly, true);
    }
}