using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class ActiveParameter : DirtyParameter<IScsActive>
{
    public override bool CheckComponentValid(SComponentBase c)
    {
        return c is IScsActive;
    }


    protected override void BuildClassStyle(IScsActive c, ClassStyleContext context)
    {
        if (c.Disabled) context.AppendClass("disabled");
        if (c.Readonly) context.AppendClass("readonly");
    }

    protected override bool CheckDirty(IScsActive c, ParameterView view)
    {
        return
            ParameterChanged(nameof(c.Readonly), c.Readonly, view) ||
            ParameterChanged(nameof(c.Disabled), c.Disabled, view)
            ;
    }
}