using System.Diagnostics;
using System.Text.Json.Serialization;
using static CSharp10.JsonSerializer.JsonSerialization;

namespace CSharp10.JsonSerializer;

public static class JsonSerializationDemo
{
    public static void Start()
    {
        var users = new TestData().Create();
        var sw = new Stopwatch();
        sw.Start();
        var json = System.Text.Json.JsonSerializer.Serialize(users, MyJsonContext.Default.IEnumerableUser);
        sw.Stop();
        Console.WriteLine($"{sw.ElapsedMilliseconds}ms");
    }
}

[JsonSerializable(typeof(IEnumerable<User>))]
internal partial class MyJsonContext : JsonSerializerContext
{
}