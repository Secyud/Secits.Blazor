using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

/// <summary>
/// 
/// </summary>
public class DirtyParameterContainer
{
    private static readonly Dictionary<Type, DirtyParameterContainer> DirtyParameters = [];

    private readonly HashSet<string> _dirtyParameters = [];
    private readonly List<Action<SBasicComp, ClassStyleBuilderContext>> _buildActions = [];

    private DirtyParameterContainer()
    {
    }

    public void AddBuildAction(Action<SBasicComp, ClassStyleBuilderContext> action)
    {
        _buildActions.Add(action);
    }

    public void AddDirtyParameter(string dirtyParameter)
    {
        _dirtyParameters.Add(dirtyParameter);
    }

    public void AddDirtyParameters(params string[] dirtyParameters)
    {
        foreach (var dirtyParameter in dirtyParameters)
        {
            AddDirtyParameter(dirtyParameter);
        }
    }

    public bool CheckDirty(ParameterView parameters)
    {
        foreach (var parameter in parameters)
        {
            if (_dirtyParameters.Contains(parameter.Name))
                return true;
        }

        return false;
    }

    public void BuildDirtyClassStyle(SBasicComp c, ClassStyleBuilderContext context)
    {
        foreach (var action in _buildActions)
        {
            action(c, context);
        }
    }
    
    public bool AddIfIs<TComponent>(SBasicComp component ,Action<TComponent, ClassStyleBuilderContext> action, params string[] parameters)
        where TComponent : class
    {
        if (component is not TComponent) return false;

        AddBuildAction((c, ctx) =>
            action((c as TComponent)!, ctx));
        AddDirtyParameters(parameters);

        return true;
    }
    public static DirtyParameterContainer GetOrCreateByComponent(SBasicComp c)
    {
        var type = c.GetType();

        if (!DirtyParameters.TryGetValue(type, out var dirtyParameter))
        {
            dirtyParameter = CreateByComponent(c);
            DirtyParameters[type] = dirtyParameter;
        }

        return dirtyParameter;
    }

    private static DirtyParameterContainer CreateByComponent(SBasicComp c)
    {
        var res = new DirtyParameterContainer();
        c.SetDirtyParameter(res);
        return res;
    }
}