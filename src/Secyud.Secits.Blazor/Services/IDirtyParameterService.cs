namespace Secyud.Secits.Blazor.Services;

public interface IDirtyParameterService
{
    IReadOnlyList<DirtyParameter> GetDirtyParameters(SComponentBase component);
}