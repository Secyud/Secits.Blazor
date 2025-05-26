using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Components;

public abstract class ScBusinessBase : ScContainerBase
{
    protected override RenderFragment? GenerateChildContent()
    {
        return null;
    }

    protected override RenderFragment GenerateContent() => builder =>
    {
        builder.AddContent(0, GenerateSettingContent());
        builder.AddContent(1, base.GenerateContent());
    };

    private RenderFragment GenerateSettingContent() => builder =>
    {
        builder.OpenComponent<CascadingValue<ScSettingMaster>>(0);
        builder.AddComponentParameter(1,
            nameof(CascadingValue<ScSettingMaster>.Value), new ScSettingMaster(this));
        builder.AddComponentParameter(2,
            nameof(CascadingValue<ScSettingMaster>.IsFixed), true);
        builder.AddComponentParameter(3,
            nameof(CascadingValue<ScSettingMaster>.ChildContent), ChildContent);
        builder.CloseComponent();
    };
}