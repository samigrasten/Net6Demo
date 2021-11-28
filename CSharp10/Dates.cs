
public class Dates
{
    internal static void Only()
    {
        var date1 = new DateOnly(2021, 12, 2);
        Console.WriteLine(date1);

        var date2 = DateOnly.FromDateTime(DateTime.Now);
        Console.WriteLine(date2);

        var time1 = new TimeOnly(18, 30);
        Console.WriteLine(time1);

        var time2 = TimeOnly.FromDateTime(DateTime.Now);
        Console.WriteLine(time2);

        var date3 = date2.AddMonths(1);
        Console.WriteLine(date3);

        var time3 = time2.AddHours(2);
        Console.WriteLine(time3);
    }
}
