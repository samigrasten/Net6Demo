namespace CSharp10.JsonSerializer
{
    internal partial class JsonSerialization
    {
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
            public string? Avatar { get; set; }
            public string? UserName { get; set; }
            public string? Email { get; set; }
            public string? SomethingUnique { get; set; }
            public string? FullName { get; set; }
            public Genders Gender { get; set; }
        }
    }
}
