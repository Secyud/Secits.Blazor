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
        context.AppendClass(c.Theme switch
        {
            Theme.Secondary => "secondary",
            Theme.Success => "success",
            Theme.Info => "info",
            Theme.Warning => "warning",
            Theme.Danger => "danger",
            Theme.Light => "light",
            Theme.Dark => "dark",
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

        if (c.Background) context.AppendClass("background");
        if (c.Borderless) context.AppendClass("borderless");
        if (c.Shadow) context.AppendClass("shadow");
        if (c.Angular) context.AppendClass("angular");
        if (c.Rounded) context.AppendClass("rounded");
    }

    public static void AppendActivable(this IActivableComponent c, ClassStyleBuilderContext context)
    {
        if (c.Disabled) context.AppendClass("disabled");
        if (c.Readonly) context.AppendClass("readonly");
    }
}