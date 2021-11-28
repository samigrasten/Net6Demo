
public class PeriodicTimerDemo
{

    public static async Task Start()
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(3));
        var counter = 1;
        while (await timer.WaitForNextTickAsync())
        {
            Console.WriteLine(DateTime.UtcNow);
            if (counter++ == 5) break;
        }
    }
}