using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class ClassStyleParameter : DirtyParameter<SComponentBase>
{
    public override bool CheckComponentValid(SComponentBase c)
    {
        return true;
    }

    protected override void BuildClassStyle(SComponentBase c, ClassStyleContext context)
    {
        context.Class.Append(c.Class);
        context.Style.Append(c.Style);
    }

    protected override bool CheckDirty(SComponentBase c, ParameterView view)
    {
        return
            ParameterChanged(nameof(c.Class), c.Class, view) ||
            ParameterChanged(nameof(c.Style), c.Style, view)
            ;
    }
}