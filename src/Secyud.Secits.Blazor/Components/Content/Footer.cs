using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class Footer : SSettingBase<SCard>, IContentFooterRenderer, IHasContent
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