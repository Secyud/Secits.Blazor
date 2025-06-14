using Microsoft.JSInterop;

namespace Secyud.Secits.Blazor.JSInterop;

public class Invoker<TEventArgs>
{
    private readonly Func<TEventArgs, Task> _func;

    public Invoker(Func<TEventArgs, Task> func)
    {
        _func = func;
    }

    [JSInvokable("Invoke")]
    public Task Invoke(TEventArgs e)
    {
        return _func(e);
    }
}

public class Invoker
{
    private readonly Func<Task> _func;

    public Invoker(Func<Task> func)
    {
        _func = func;
    }

    [JSInvokable("Invoke")]
    public Task Invoke()
    {
        return _func();
    }
}