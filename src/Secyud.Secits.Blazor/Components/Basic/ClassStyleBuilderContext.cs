using System.Text;

namespace Secyud.Secits.Blazor;

public class ClassStyleBuilderContext
{
    public StringBuilder Class { get; } = new();

    public StringBuilder Style { get; } = new();

    public void Clear()
    {
        Class.Clear();
        Style.Clear();
    }

    public void AppendClass(string? @class, params string[] parameters)
    {
        if (string.IsNullOrWhiteSpace(@class)) return;
        Class.Append(' ');
        Class.Append(@class);
        foreach (var parameter in parameters)
            Class.Append(parameter);
    }

    public void AppendStyle(string name, string? value, bool important = false)
    {
        if (string.IsNullOrWhiteSpace(value)) return;
        
        Style.Append(name);
        Style.Append(':');
        Style.Append(value);
        if (important)
            Style.Append(" !important");
        Style.Append(';');
    }

    public void AppendClassOrStyle(IMaybeClassParameter parameter, string classPrefix, string styleName)
    {
        if (parameter.IsClass)
            AppendClass(classPrefix, parameter.Value);
        else
            AppendStyle(styleName, parameter.Value);
    }
}