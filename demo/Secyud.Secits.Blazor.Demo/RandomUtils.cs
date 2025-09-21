using System.Text;

namespace Secyud.Secits.Blazor;

public class RandomUtils
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

    public static int Rand(int max)
    {
        return Random.Next(max);
    }

    public static int Rand(int min, int max)
    {
        return Random.Next(min, max);
    }

    public static string GetString(params string[] arr)
    {
        var sb = new StringBuilder();
        sb.Clear();
        RandomString(sb, arr);
        return sb.ToString();
    }

    public static string GetWord(bool firstUpper = true)
    {
        var sb = new StringBuilder();
        sb.Clear();
        RandomWord(sb, firstUpper);
        return sb.ToString();
    }

    public static string GetSentence()
    {
        var sb = new StringBuilder();
        sb.Clear();
        RandomSentence(sb);
        return sb.ToString();
    }

    public static string GetSentences(int count = 10)
    {
        var sb = new StringBuilder();
        sb.Clear();
        RandomSentences(sb, count);
        return sb.ToString();
    }

    private static void RandomString(StringBuilder sb, params string[] arr)
    {
        foreach (var str in arr)
        {
            var random = Random.Next(str.Length);
            sb.Append(str[random]);
        }
    }

    private static void RandomWord(StringBuilder sb, bool firstUpper)
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

        RandomString(sb, arr);
    }

    private static void RandomSentence(StringBuilder sb)
    {
        RandomWord(sb, true);
        var len = Random.Next(3, 10);
        for (var i = 0; i < len; i++)
        {
            sb.Append(' ');
            RandomWord(sb, false);
        }

        sb.Append(". ");
    }

    private static void RandomSentences(StringBuilder sb, int count)
    {
        for (var i = 0; i < count; i++)
        {
            RandomSentence(sb);
        }
    }
}