namespace Secyud.Secits.Blazor.Data;

public class ItemBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = RandomUtils.GetWord();
    public string Description { get; set; } = RandomUtils.GetSentence();
    public string PhoneNumber { get; set; } = RandomUtils.GetString(
        "1", RandomUtils.NumChars, RandomUtils.NumChars, RandomUtils.NumChars, RandomUtils.NumChars, RandomUtils.NumChars,
        RandomUtils.NumChars, RandomUtils.NumChars, RandomUtils.NumChars, RandomUtils.NumChars, RandomUtils.NumChars);

    public int Age { get; set; } = RandomUtils.Rand(18, 100);

    public DateTime BirthDate { get; set; } = new DateTime(1990, 1, 1).AddDays(RandomUtils.Rand(-100000, 100000));
}