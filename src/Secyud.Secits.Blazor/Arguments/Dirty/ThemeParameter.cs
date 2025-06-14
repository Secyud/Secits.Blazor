using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class ThemeParameter : DirtyParameter<IHasTheme>
{
    public override bool CheckComponentValid(SComponentBase c)
    {
        return c is IHasTheme;
    }


    protected override void BuildClassStyle(IHasTheme c, ClassStyleContext context)
    {
        context.AppendClass(c.Theme switch
        {
            Theme.Primary => "primary",
            Theme.Secondary => "secondary",
            Theme.Naive => "naive",
            Theme.Success => "success",
            Theme.Info => "info",
            Theme.Warning => "warning",
            Theme.Danger => "danger",
            _ => null
        });

        context.AppendClass(c.Size switch
        {
            Size.XSmall => "x-small",
            Size.Small => "small",
            Size.Large => "large",
            Size.XLarge => "x-large",
            _ => null
        });

        if (c.StyleOption.HasFlag(Style.Background)) context.AppendClass("bg");
        if (c.StyleOption.HasFlag(Style.Borderless)) context.AppendClass("bl");
        if (c.StyleOption.HasFlag(Style.Shadow)) context.AppendClass("sd");
        if (c.StyleOption.HasFlag(Style.Angular)) context.AppendClass("ag");
        if (c.StyleOption.HasFlag(Style.Rounded)) context.AppendClass("rd");
        if (c.StyleOption.HasFlag(Style.Plain)) context.AppendClass("pl");
    }

    protected override bool CheckDirty(IHasTheme c, ParameterView view)
    {
        return
            ParameterChanged(nameof(c.Size), c.Size, view) ||
            ParameterChanged(nameof(c.Theme), c.Theme, view) ||
            ParameterChanged(nameof(c.StyleOption), c.StyleOption, view)
            ;
    }
}