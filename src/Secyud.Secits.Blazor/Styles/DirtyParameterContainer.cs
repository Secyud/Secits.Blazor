using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Components;

namespace Secyud.Secits.Blazor.Styles;

/// <summary>
/// Represents a container for managing dirty parameters and associated build actions for components.
/// This class is used to track parameters that are considered "dirty" and execute specific actions
/// when these parameters are detected. It provides methods to add, check, and manage dirty parameters,
/// as well as to build class and style modifications based on the component's state.
/// </summary>
public class DirtyParameterContainer
{
    private static readonly Dictionary<Type, DirtyParameterContainer> DirtyParameters = [];

    private readonly HashSet<string> _dirtyParameters = [];
    private readonly List<Action<ScStyledBase, ClassStyleBuilderContext>> _buildActions = [];

    private DirtyParameterContainer()
    {
    }

    public void AddBuildAction(Action<ScStyledBase, ClassStyleBuilderContext> action)
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

    public void BuildDirtyClassStyle(ScStyledBase c, ClassStyleBuilderContext context)
    {
        foreach (var action in _buildActions)
        {
            action(c, context);
        }
    }
    
    public bool AddIfIs<TComponent>(ScStyledBase component ,Action<TComponent, ClassStyleBuilderContext> action, params string[] parameters)
        where TComponent : class
    {
        if (component is not TComponent) return false;

        AddBuildAction((c, ctx) =>
            action((c as TComponent)!, ctx));
        AddDirtyParameters(parameters);

        return true;
    }
    
    public static DirtyParameterContainer GetOrCreateByComponent(ScStyledBase c)
    {
        var type = c.GetType();

        if (!DirtyParameters.TryGetValue(type, out var dirtyParameter))
        {
            dirtyParameter = CreateByComponent(c);
            DirtyParameters[type] = dirtyParameter;
        }

        return dirtyParameter;
    }

    private static DirtyParameterContainer CreateByComponent(ScStyledBase c)
    {
        var res = new DirtyParameterContainer();
        c.SetDirtyParameter(res);
        return res;
    }
}