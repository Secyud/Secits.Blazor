using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class SHeader : SSettingBase<SCard>, ISciHeaderRenderer, ISchContent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override void ApplySetting()
    {
        Master.Header.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.Header.Forgo(this);
    }

    public RenderFragment? GenerateHeader() => ChildContent;
}