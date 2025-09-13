using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class Footer : SPluginBase<SCard>, IContentRenderer, IHasContent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    protected override void ApplySetting()
    {
        Master.Content.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.Content.Forgo(this);
    }


    public RenderFragment? RenderTemplate() => ChildContent;

    public RendererPosition GetLayoutPosition()
    {
        return RendererPosition.Footer;
    }
}