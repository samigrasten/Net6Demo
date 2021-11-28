
public class Linq
{
    internal static void BatchingSequences()
    {
        var users = new TestData().CreateUsers(1000);

        var chuncks = users.Chunk(100);

        Console.WriteLine($"{users.Count} users in {chuncks.Count()} chuncks");
    }

    internal static void ByOperators()
    {
        var users1 = new TestData().CreateUsers(30);
        var users2 = new TestData().CreateUsers(20);
        users2.AddRange(users1.Take(10));

        var result1 = users1.DistinctBy(x => x.FirstName).ToList();
        var result2 = users1.ExceptBy(users2.Select(x => x.FullName), u => u.FullName).ToList();
        var result3 = users1.IntersectBy(users2.Select(x => x.FullName), u => u.FullName).ToList();
        var result4 = users1.UnionBy(users2, u => u.FullName).ToList();

        var result5 = users1.MinBy(u => u.Age);
        var result6 = users1.MaxBy(u => u.Age);
    }

    internal static void DefaultsValues()
    {
        var users = new TestData().CreateUsers(30);

        var result1 = users.FirstOrDefault(u => u.Age == 18, users.First());
        var result2 = users.LastOrDefault(u => u.Age == 100, users.Last());
        var result3 = users.SingleOrDefault(u => u.Id == 31, users.First());
    }

    internal static void IndexAndRange()
    {
        var users = new TestData().CreateUsers(30);

        var result1 = users.Take(10..20).ToList();
        var result2 = users.Take(..20).ToList();
        var result3 = users.Take(10..).ToList();
        var result4 = users.Take(^5..).ToList();
        var resul5 = users.Take(10..20).ToList();

        var resul6 = users.ElementAt(^4);
    }    
}
