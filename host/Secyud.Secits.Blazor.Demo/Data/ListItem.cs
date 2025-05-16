namespace Secyud.Secits.Blazor.Demo.Data;

public class ListItem : ItemBase
{
    public static List<ListItem> Generate()
    {
        var res = new List<ListItem>();
        for (var i = 0; i < 20; i++)
        {
            res.Add(new ListItem());
        }

        return res;
    }
}