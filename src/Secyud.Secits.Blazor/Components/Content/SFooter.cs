using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class SFooter : SSettingBase<SCard>, ISciFooterRenderer, ISchContent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override void ApplySetting()
    {
        Master.Footer.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.Footer.Forgo(this);
    }

    public RenderFragment? GenerateFooter() => ChildContent;
}