using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Net6Demo.Models
{
    public class ToDoDb : DbContext
    {
        private string _connectionstring;

        public ToDoDb(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        
        public ToDoDb(IConfiguration options)
        {
            _connectionstring = options.GetValue<string>("ToDoConnectionstring");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionstring);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ToDoItem>()
                .ToTable("ToDoItem", b => b.IsTemporal());
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
