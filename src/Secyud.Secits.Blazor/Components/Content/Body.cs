using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class Body : SSettingBase<SCard>, IContentBodyRenderer, IHasContent
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