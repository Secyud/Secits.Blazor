using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class HeightParameter : DirtyParameter<IScsHeight>
{
    public override bool CheckComponentValid(SComponentBase c)
    {
        return c is IScsHeight and not IScsSize;
    }

    public static void Append(IScsHeight c, ClassStyleContext context)
    {
        context.AppendClassOrStyle(c.Height, "h-", "height");
    }

    protected override void BuildClassStyle(IScsHeight c, ClassStyleContext context)
    {
        Append(c, context);
    }

    protected override bool CheckDirty(IScsHeight c, ParameterView view)
    {
        return ParameterChanged(nameof(c.Height), c.Height, view);
    }
}