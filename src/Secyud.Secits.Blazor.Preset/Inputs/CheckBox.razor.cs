using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor.Preset;

public partial class CheckBox
{
    [Parameter]
    public bool SubmitOnInput { get; set; }

    [Parameter]
    public EventCallback<bool> CheckedChanged { get; set; }

    private bool _checked;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        _checked = Checked;
    }

    protected override void OnClick(MouseEventArgs args)
    {
        base.OnClick(args);
        _checked = !_checked;
        CheckedChanged.InvokeAsync(_checked).ConfigureAwait(false);
    }
}