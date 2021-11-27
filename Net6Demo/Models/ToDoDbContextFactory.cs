using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Net6Demo.Models
{
    public class ToDoDbContextFactory : IDesignTimeDbContextFactory<ToDoDb>
    {
        public ToDoDb CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ToDoDb>();
            var connectionString = "Data Source=DESKTOP-1Q3USS2;Initial Catalog=todo;Integrated Security=SSPI";
            //optionsBuilder.UseSqlServer(connectionString);           
            return new ToDoDb(connectionString);
        }
    }
}
