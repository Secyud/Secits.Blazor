using System.Text;

namespace Secyud.Secits.Blazor.Demo;

public class Utils
{
    public const string AllChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    public const string NumChars = "1234567890";
    public const string LowChars = "abcdefghijklmnopqrstuvwxyz";
    public const string LowIChars = "aeiou";
    public const string LowUChars = "bcdfghjklmnpqrstvwxyz";
    public const string UpChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string UpIChars = "AEIOU";
    public const string UpUChars = "BCDFGHJKLMNPQRSTVWXYZ";

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

    public static string GetRandomString(params string[] arr)
    {
        Sb.Clear();

        foreach (var str in arr)
        {
            var random = Random.Next(str.Length);
            Sb.Append(str[random]);
        }

        return Sb.ToString();
    }


    public static string GetRandomWord(bool firstUpper = true)
    {
        var len = Random.Next(3, 10);
        var arr = new string[len];
        for (var i = 0; i < len; i++)
        {
            if (i == 0 && firstUpper)
            {
                arr[i] = UpUChars;
                continue;
            }

            arr[i] = i % 2 == 0 ? LowUChars : LowIChars;
        }

        return GetRandomString(arr);
    }

    public static string GetRandomSentence()
    {
        var sb = new StringBuilder();

        sb.Append(GetRandomWord());

        var len = Random.Next(3, 10);
        for (int i = 0; i < len; i++)
        {
            sb.Append(' ');
            sb.Append(GetRandomWord(false));
        }

        sb.Append(". ");
        return sb.ToString();
    }

    public static string GetRandomString(int length)
    {
        Sb.Clear();

        for (var i = 0; i < length; i++)
        {
            var random = Random.Next(AllChars.Length * 4 / 3);
            Sb.Append(random >= AllChars.Length
                ? ' '
                : AllChars[random]);
        }

        return Sb.ToString();
    }
}