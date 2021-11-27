using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

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

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
