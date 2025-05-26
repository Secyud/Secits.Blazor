using Secyud.Secits.Blazor.Components;
using Secyud.Secits.Blazor.Styles;

namespace Secyud.Secits.Blazor.Arguments;

public static class ParameterExtensions
{
    public static void AppendWidth(this IScsWidth c, ClassStyleBuilderContext context)
    {
        context.AppendClassOrStyle(c.Width, "w-", "width");
    }

    public static void AppendHeight(this IScsHeight c, ClassStyleBuilderContext context)
    {
        context.AppendClassOrStyle(c.Height, "h-", "height");
    }

    public static void AppendSize(this IScsSize c, ClassStyleBuilderContext context)
    {
        if (c.Height.IsClass && c.Width.IsClass && c.Height.Value == c.Width.Value)
            context.AppendClass("size-", c.Height.ToString());
        else
        {
            c.AppendWidth(context);
            c.AppendHeight(context);
        }
    }

    public static void AppendTheme(this IScsTheme c, ClassStyleBuilderContext context)
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

    public static void AppendActivable(this IScsActive c, ClassStyleBuilderContext context)
    {
        if (c.Disabled) context.AppendClass("disabled");
        if (c.Readonly) context.AppendClass("readonly");
    }
}