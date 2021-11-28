
public class ZipDemo { 

    public static void Start()
    {
        var users = new TestData().CreateUsers(10);
        var names = users.Select(user => user.FullName).ToList();
        var usernames = users.Select(user => user.UserName).ToList();
        var emails  = users.Select(user => user.Email).ToList();

        foreach ((string name, string username, string email) in names.Zip(usernames, emails))
        {
            Console.WriteLine($"{name} - {username} - {email}");
        }        
    }
}
