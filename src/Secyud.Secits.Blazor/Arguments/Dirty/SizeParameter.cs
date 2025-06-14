using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class SizeParameter : DirtyParameter<IScsSize>
{
    public override bool CheckComponentValid(SComponentBase c)
    {
        return c is IScsSize;
    }

    protected override void BuildClassStyle(IScsSize c, ClassStyleContext context)
    {
        if (c.Height.IsClass && c.Width.IsClass && c.Height.Value == c.Width.Value)
            context.AppendClass("size-", c.Height.ToString());
        else
        {
            WidthParameter.Append(c, context);
            HeightParameter.Append(c, context);
        }
    }

    protected override bool CheckDirty(IScsSize c, ParameterView view)
    {
        return
            ParameterChanged(nameof(c.Width), c.Width, view) ||
            ParameterChanged(nameof(c.Height), c.Height, view)
            ;
    }
}