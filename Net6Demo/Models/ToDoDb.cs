using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Net6Demo.Models
{
    public class ToDoDb : DbContext
    {
        private readonly IConfiguration _options;

        public ToDoDb(IConfiguration options)
        {
            _options = options;            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_options.GetValue<string>("ToDoConnectionstring"));            
        }

        public ISet<ToDoItem> ToDoItems { get; set; }
    }
}
