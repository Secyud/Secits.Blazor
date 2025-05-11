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

    public static void AppendColor(this IColorComponent c, ClassStyleBuilderContext context)
    {
        context.AppendClass(c.Color switch
        {
            ColorType.Primary => "primary",
            ColorType.Secondary => "secondary",
            ColorType.Success => "success",
            ColorType.Info => "info",
            ColorType.Warning => "warning",
            ColorType.Danger => "danger",
            ColorType.Light => "light",
            ColorType.Dark => "dark",
            _ => null
        });
    }
}