using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class WidthParameter : DirtyParameter<IScsWidth>
{
    public override bool CheckComponentValid(SComponentBase c)
    {
        return c is IScsWidth and not IScsSize;
    }

    public static void Append(IScsWidth c, ClassStyleContext context)
    {
        context.AppendClassOrStyle(c.Width, "w-", "width");
    }

    protected override void BuildClassStyle(IScsWidth c, ClassStyleContext context)
    {
        Append(c, context);
    }

    protected override bool CheckDirty(IScsWidth c, ParameterView view)
    {
        return ParameterChanged(nameof(c.Width), c.Width, view);
    }
}