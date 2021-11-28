
public class PriorityQueueDemo { 
    public static void Start()
    {
        var users = new TestData().CreateUsers(10);
        var queue = new PriorityQueue<User, int?>();

        Console.WriteLine("Adding 10 users");
        users.ForEach(user => queue.Enqueue(user, user.Age));

        DequeueUsers(5);

        Console.WriteLine();
        Console.WriteLine("Adding 3 young users");
        var youngUser1 = new User(11) { FullName = "young user 1", Age = 14 };
        var youngUser2 = new User(12) { FullName = "young user 2", Age = 17 };
        var youngUser3 = new User(13) { FullName = "young user 3", Age = 15 };
        queue.Enqueue(youngUser1, youngUser1.Age);
        queue.Enqueue(youngUser2, youngUser2.Age);
        queue.Enqueue(youngUser3, youngUser3.Age);

        DequeueUsers(8);

        void DequeueUsers(int count)
        {
            Console.WriteLine();
            Console.WriteLine($"Listing first {count}:");
            Enumerable.Range(1, count).ToList().ForEach(_ => {
                var item = queue.Dequeue();
                Console.WriteLine(item.FullName + ", age " + item.Age);
            });
        }
    }    
}
