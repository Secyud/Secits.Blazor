using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public interface IInputInvokable<TValue> : IPluggable
{
    SSetting<IInputInvoker<TValue>> InputInvoker { get; }
    
    Task OnValueChangedAsync(TValue value);
}