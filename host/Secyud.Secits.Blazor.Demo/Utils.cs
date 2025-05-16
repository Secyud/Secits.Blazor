using System.Text;

namespace Secyud.Secits.Blazor.Demo;

public class Utils
{
    private const string Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    private static readonly Random Random = new();
    private static readonly StringBuilder Sb = new();

    public static int Rand(int max)
    {
        return Random.Next(max);
    }

    public static int Rand(int min, int max)
    {
        return Random.Next(min, max);
    }

    public static string GetRandomString(int length)
    {
        Sb.Clear();

        for (var i = 0; i < length; i++)
        {
            var random = Random.Next(Chars.Length * 4 / 3);
            Sb.Append(random >= Chars.Length
                ? ' '
                : Chars[random]);
        }

        return Sb.ToString();
    }
}