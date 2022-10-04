namespace DB_Test.Additional_Classes;

public static class Randomizer
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static readonly Random Rnd = new();

    public static DateOnly RandomDatebirth()
    {
        var fromDate = new DateTime(1980, 1, 1);
        var toDate = new DateTime(2020, 12, 31);
        var range = (toDate - fromDate).Days;
        return DateOnly.FromDateTime(fromDate.AddDays(Rnd.Next(range)));
    }

    public static string RandomName()
    {
        var len = Rnd.Next(5, 12);
        return new string(Enumerable.Repeat(Chars, len).Select(s => s[Rnd.Next(len)]).ToArray());
    }

    public static bool RandomSex()
    {
        return Rnd.Next(0, 2) == 0;
    }
}