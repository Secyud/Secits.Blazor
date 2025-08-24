using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class ParameterContainer(ParameterView parameterView)
{
    private readonly List<Task> _parameterTasks = [];
    public ParameterView ParameterView { get; } = parameterView;
    public IReadOnlyList<Task> ParameterTasks => _parameterTasks;

    public void UseParameter<TParameter>(TParameter previous, string name, Action<TParameter> action)
    {
        if (TryGetParameterChanged(previous, name, out var value))
            action(value);
    }

    public void UseParameter<TParameter>(TParameter previous, string name, Func<TParameter, Task> action)
    {
        if (TryGetParameterChanged(previous, name, out var value))
            _parameterTasks.Add(action(value));
    }

    private bool TryGetParameterChanged<TParameter>(TParameter previous, string name, out TParameter value)
    {
        if (ParameterView.TryGetValue<TParameter>(name, out var v))
        {
            value = v!;
            return !Equals(previous, value);
        }

        value = default!;
        return false;
    }
}