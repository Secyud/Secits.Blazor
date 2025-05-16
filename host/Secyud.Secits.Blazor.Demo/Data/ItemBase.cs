using System.Text;

namespace Secyud.Secits.Blazor.Demo.Data;

public class ItemBase
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int Sequence { get; set; } = Utils.Rand(100000);

    public string Name { get; set; } = Utils.GetRandomString(10);

    public string Description { get; set; } = Utils.GetRandomString(40);

    public DateTime CreateTime { get; set; } = DateTime.Now.AddTicks(Utils.Rand(-1000000, 1000000));
}