using System;

namespace Net6Demo.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
