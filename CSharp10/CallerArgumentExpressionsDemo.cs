
using System.Runtime.CompilerServices;

public class CallerArgumentExpressionsDemo
{
    public static void Start()
    {
        Console.WriteLine("Invoking first method");
        InvokeMethod(64, "SomeString");

        Console.WriteLine();
        Console.WriteLine("Invoking second method");
        InvokeMethod(64, null);

        Console.WriteLine();
        Console.WriteLine("Invoking third method");
        InvokeMethod(130, "SomeString");
    }

    private static void InvokeMethod(int intvalue, string? stringValue)
    {        
        stringValue.NotNull();
        intvalue.GreaterThanZero();
        intvalue.LessThan(100);

        // Do something with params
    }
}

public static class GuardExtensions
{
    public static void NotNull(this string? value, [CallerArgumentExpression("value")] string valueExpression = null)
    {
        if (value == null) Console.WriteLine($"{valueExpression} was null");
    }

    public static void GreaterThanZero(this int value, [CallerArgumentExpression("value")] string valueExpression = null)
    {
        if (value <= 0) Console.WriteLine($"{valueExpression} was zero or below");
    }

    public static void LessThan(this int value, int limit, [CallerArgumentExpression("value")] string valueExpression = null, [CallerArgumentExpression("limit")] string limitExpression = null)
    {
        if (value >= limit) Console.WriteLine($"{valueExpression} was greater than {limit}");
    }
} 