using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class Header : SSettingBase<SCard>, IContentHeaderRenderer, IHasContent
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