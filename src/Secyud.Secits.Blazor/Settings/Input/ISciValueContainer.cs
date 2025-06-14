namespace Secyud.Secits.Blazor;

public interface ISciValueContainer<in TValue>
{
    public Task SetValueFromParameterAsync(TValue value)
    {
        return Task.CompletedTask;
    }
}