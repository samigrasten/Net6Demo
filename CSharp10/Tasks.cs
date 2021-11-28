
public class Tasks
{
    private Func<object, CancellationToken, ValueTask> options;

    public async Task WaitAsyncDemo()
    {
        Console.WriteLine("Waiting for 5 seconds...");
        await (LongRunningWorkAsync("Task 1", 1000).WaitAsync(TimeSpan.FromMinutes(1)));
        Console.WriteLine("Ready");
        Console.ReadLine();

        try
        {
            Console.WriteLine();
            Console.WriteLine("Waiting for 5 seconds...");
            await LongRunningWorkAsync("Task 2", 10000).WaitAsync(TimeSpan.FromSeconds(2));
            Console.WriteLine("Ready");
        }
        catch (TimeoutException)
        {
            Console.WriteLine("TimeOut");
        }
        Console.ReadLine();

        try
        {
            Console.WriteLine();
            Console.WriteLine("Waiting for cancellation");
            var cts = new CancellationTokenSource();
            CancelAfterWait(2000, cts);

            await LongRunningWorkAsync("Task 2", 60000).WaitAsync(cts.Token);
            Console.WriteLine("Ready");
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("Cancelled");
        }
        Console.ReadLine();
    }

    public async Task ParallelForEach()
    {
        var urlsToDownload = new[]
        {
            "https://httpstat.us/200",
            "https://httpstat.us/201",
            "https://httpstat.us/202",            
        };

        var client = new HttpClient();
        var options = new ParallelOptions { MaxDegreeOfParallelism = 3 };
        await Parallel.ForEachAsync(urlsToDownload, options, async (url, token) =>
        {
            Console.WriteLine($"Request to : {url}");
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Statuscode: {response.StatusCode}");
            }
        });
    }

    private async Task LongRunningWorkAsync(string name, int delay)
    {
        await Task.Delay(delay);
        Console.WriteLine($"{name} completed");
    }

    private async Task CancelAfterWait(int delay, CancellationTokenSource cts)
    {
        await Task.Delay(delay);
        Console.WriteLine($"Cancelling now...");
        cts.Cancel();
    }
}
