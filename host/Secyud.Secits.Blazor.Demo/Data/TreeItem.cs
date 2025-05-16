namespace Secyud.Secits.Blazor.Demo.Data;

public class TreeItem : ItemBase
{
    public Guid? ParentId { get; set; }

    public List<TreeItem> Children { get; set; } = [];

    public static List<TreeItem> Generate(int itemCount = 20, bool hierarchy = false)
    {
        var res = new List<TreeItem>();
        for (var i = 0; i < itemCount; i++)
        {
            var item = new TreeItem();
            res.Add(item);
            var index = Utils.Rand(-1, i);
            if (index == -1) continue;
            var parent = res[index];
            item.ParentId = parent.Id;
            parent.Children.Add(item);
        }

        if (hierarchy)
        {
            res = res.Where(u => u.ParentId is null).ToList();
        }

        return res;
    }
}