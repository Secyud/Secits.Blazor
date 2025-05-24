using System.Globalization;
using System.Numerics;

namespace Secyud.Secits.Blazor.Components;

public partial class SInputNumber<TValue> where TValue : struct, INumber<TValue>
{
    protected override bool TryParseValueFromString(string? value, out TValue output)
    {
        return TValue.TryParse(value, NumberFormatInfo.InvariantInfo, out output);
    }
}