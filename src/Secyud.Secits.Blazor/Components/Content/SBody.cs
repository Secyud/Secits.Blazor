using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class SBody : SSettingBase<SCard>, ISciBodyRenderer, ISchContent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override void ApplySetting()
    {
        Master.Body.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.Body.Forgo(this);
    }

    public RenderFragment? GenerateBody() => ChildContent;
}