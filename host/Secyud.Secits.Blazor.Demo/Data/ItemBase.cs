namespace Secyud.Secits.Blazor.Demo.Data;

public class ItemBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = Utils.GetRandomWord();
    public string Description { get; set; } = Utils.GetRandomSentence();

    public string PhoneNumber { get; set; } = Utils.GetRandomString(
        "1", "357", "13579", Utils.NumChars, Utils.NumChars, Utils.NumChars,
        Utils.NumChars, Utils.NumChars, Utils.NumChars, Utils.NumChars, Utils.NumChars);

    public int Age { get; set; } = Utils.Rand(18, 100);
    
    public DateTime BirthDate { get; set; } = new DateTime(1990, 1, 1).AddDays(Utils.Rand(-100000, 100000));
}