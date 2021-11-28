using Bogus;
using static User;

public class TestData
{
    public List<User> CreateUsers(int amount = 1500000)
    {
        var userIds = 0;
        var testUsers = new Faker<User>()
            .CustomInstantiator(f => new User(userIds++))
            .RuleFor(u => u.Gender, f => f.PickRandom<Genders>())
            .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName())
            .RuleFor(u => u.LastName, (f, u) => f.Name.LastName())
            .RuleFor(u => u.Age, f => f.Random.Int(18,100))
            .RuleFor(u => u.Avatar, f => f.Internet.Avatar())
            .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
            .RuleFor(u => u.SomethingUnique, f => $"Value {f.UniqueIndex}")
            .RuleFor(u => u.FullName, (f, u) => u.FirstName + " " + u.LastName);

        return Enumerable.Range(1, amount).Select(_ => testUsers.Generate()).ToList();
    }
}

public class User
{
    public enum Genders
    {
        Male,
        Female
    }
    public User(int id) => Id = id;

    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? Age { get; set; }
    public string? Avatar { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? SomethingUnique { get; set; }
    public string? FullName { get; set; }
    public Genders Gender { get; set; }
}
