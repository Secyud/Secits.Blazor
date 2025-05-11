namespace Secyud.Secits.Blazor.Components.Pages;

public class TestView
{
    public string? Name { get; set; }

    public static List<TestView> GetTestViews()
    {
        return
        [
            new TestView()
            {
                Name = "1"
            },
            new TestView()
            {
                Name = "2"
            },
            new TestView()
            {
                Name = "3"
            },
            new TestView()
            {
                Name = "4"
            },
        ];
    }
}