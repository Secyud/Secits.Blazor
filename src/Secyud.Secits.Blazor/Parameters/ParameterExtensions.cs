namespace Secyud.Secits.Blazor;

public static class ParameterExtensions
{
    public static void AppendWidth(this IWidthComponent c, ClassStyleBuilderContext context)
    {
        context.AppendClassOrStyle(c.Width, "w-", "width");
    }

    public static void AppendHeight(this IHeightComponent c, ClassStyleBuilderContext context)
    {
        context.AppendClassOrStyle(c.Height, "h-", "height");
    }

    public static void AppendSize(this ISizeComponent c, ClassStyleBuilderContext context)
    {
        if (c.Height.IsClass && c.Width.IsClass && c.Height.Value == c.Width.Value)
            context.AppendClass("size-", c.Height.ToString());
        else
        {
            c.AppendWidth(context);
            c.AppendHeight(context);
        }
    }

    public static void AppendTheme(this IThemeComponent c, ClassStyleBuilderContext context)
    {
        var color = STheme.Color & c.Theme;

        context.AppendClass(color switch
        {
            STheme.Primary => "primary",
            STheme.Secondary => "secondary",
            STheme.Success => "success",
            STheme.Info => "info",
            STheme.Warning => "warning",
            STheme.Danger => "danger",
            STheme.Light => "light",
            STheme.Dark => "dark",
            _ => null
        });

        var style = STheme.Style & c.Theme;

        context.AppendClass(style switch
        {
            STheme.Outlined => "outlined",
            STheme.NoBorder => "no-border",
            _ => null
        });

        var size = STheme.Size & c.Theme;

        context.AppendClass(size switch
        {
            STheme.XSmall => "x-small",
            STheme.Small => "small",
            STheme.Large => "large",
            STheme.XLarge => "x-large",
            _ => null
        });

        if (c.Theme.HasFlag(STheme.Shadow))
        {
            context.AppendClass("shadow");
        }

        if (c.Theme.HasFlag(STheme.Angular))
        {
            context.AppendClass("angular");
        }

        if (c.Theme.HasFlag(STheme.Rounded))
        {
            context.AppendClass("rounded");
        }
    }
}