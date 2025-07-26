using Microsoft.Extensions.Options;
using Secyud.Secits.Blazor.Options;

namespace Secyud.Secits.Blazor.Services;

public class DirtyParameterService(IOptions<SecitsOptions> options):IDirtyParameterService
{
    private readonly Dictionary<Type, List<DirtyParameter>> _parameters = [];

    public IReadOnlyList<DirtyParameter> GetDirtyParameters(SComponentBase component)
    {
        var type = component.GetType();
        if (!_parameters.TryGetValue(type, out var list))
        {
            list = [];

            _parameters[type] = list;

            list.AddRange(options.Value.Parameters
                .Where(parameter => parameter.CheckComponentValid(component)));
        }

        return list;
    }
}