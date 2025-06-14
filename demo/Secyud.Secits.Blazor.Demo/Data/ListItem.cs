namespace Secyud.Secits.Blazor.Data;

public class ListItem : ItemBase
{
    public static List<ListItem> Generate(int count = 20)
    {
        var res = new List<ListItem>();
        for (var i = 0; i < count; i++)
        {
            res.Add(new ListItem());
        }

        return res;
    }
}