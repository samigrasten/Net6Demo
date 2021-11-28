
public class RecordStructDemo
{
    public static void Equality()
    {
        var fiveEuros = new Money(5.0m, "€");
        var fiveEuros2 = new Money(5.0m, "€");
        var fiveDollars = new Money(5.0m, "$");
        Console.WriteLine($"fiveEuros = fiveEuros2: {fiveEuros == fiveEuros2}");
        Console.WriteLine($"fiveEuros = fiveDollars: {fiveEuros == fiveDollars}");

        var tenEuros = new Money(10.0m, "€");
        Console.WriteLine($"tenEuros = fiveEuros: {tenEuros == fiveEuros}");

        var tenEuros2 = fiveEuros with { Amount = 10.0m };
        Console.WriteLine($"tenEuros = tenEuros2: {tenEuros == tenEuros2}");
    }
    
    public static void Mutability()
    {
        var fiveEuros = new Money(5.0m, "eur");
        fiveEuros.Amount = 15.0m;
        Console.WriteLine(fiveEuros);

        var tenEuros = new ImmutableMoney(10.0m, "eur");
        //tenEuros.Amount = 15.0m;
        Console.WriteLine(tenEuros);
    }
}

public record struct Money(decimal Amount, string Currency);

public readonly record struct ImmutableMoney(decimal Amount, string Currency);
