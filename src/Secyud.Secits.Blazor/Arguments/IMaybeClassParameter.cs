namespace Secyud.Secits.Blazor.Arguments;

public interface IMaybeClassParameter
{
    public bool IsClass { get; }
    public string Value { get; }
}