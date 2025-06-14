using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public abstract class DirtyParameter
{
    public abstract bool CheckComponentValid(SComponentBase c);
    public abstract bool CheckComponentDirty(SComponentBase component, ParameterView view);
    public abstract void BuildComponentClassStyle(SComponentBase c, ClassStyleContext context);
}

public abstract class DirtyParameter<TComponent> : DirtyParameter where TComponent : class
{
    protected bool ParameterChanged<TParameter>(string name, TParameter value, ParameterView view)
    {
        if (view.TryGetValue<TParameter>(name, out var p))
            return !Equals(p, value);

        return false;
    }

    protected abstract void BuildClassStyle(TComponent c, ClassStyleContext context);

    protected abstract bool CheckDirty(TComponent c, ParameterView view);

    public sealed override void BuildComponentClassStyle(SComponentBase c, ClassStyleContext context)
    {
        BuildClassStyle((c as TComponent)!, context);
    }

    public sealed override bool CheckComponentDirty(SComponentBase component, ParameterView view)
    {
        return CheckDirty((component as TComponent)!, view);
    }
}