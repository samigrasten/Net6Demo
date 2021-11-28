using Bogus;
using static CSharp10.JsonSerializer.JsonSerialization.User;

namespace CSharp10.JsonSerializer
{
    internal partial class JsonSerialization
    {
        public class TestData
        {
            public List<User> Create()
            {
                var userIds = 0;
                var testUsers = new Faker<User>()
                    .CustomInstantiator(f => new User(userIds++))
                    .RuleFor(u => u.Gender, f => f.PickRandom<Genders>())
                    .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName())
                    .RuleFor(u => u.LastName, (f, u) => f.Name.LastName())
                    .RuleFor(u => u.Avatar, f => f.Internet.Avatar())
                    .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
                    .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                    .RuleFor(u => u.SomethingUnique, f => $"Value {f.UniqueIndex}")
                    .RuleFor(u => u.FullName, (f, u) => u.FirstName + " " + u.LastName);

                return Enumerable.Range(1, 1500000).Select(_ => testUsers.Generate()).ToList();
            }
        }
    }
}
