namespace Secyud.Secits.Blazor;

public readonly struct SColor(string value, bool isClass)
{
    public string Value { get; } = value;
    public bool IsClass { get; } = isClass;

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator SColor(string str)
    {
        if (str.StartsWith('.'))
            return new SColor(str[1..], true);

        return new SColor(str, false);
    }

    public static SColor Class(string cls) => new(cls, true);

    public static SColor Var(string value) => new($"var({value})", false);

    public static SColor Primary => Class("primary");
    public static SColor Secondary => Class("secondary");
    public static SColor Success => Class("success");
    public static SColor Info => Class("info");
    public static SColor Warning => Class("warning");
    public static SColor Danger => Class("danger");
    public static SColor Light => Class("light");
    public static SColor Dark => Class("dark");
}