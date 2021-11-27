

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.MapGet("/", () => ".NET Conf Kuopio 2021");

var items = new List<ToDoItem>
{
    new ToDoItem()
    {
        Id = 1,
        Date = DateTime.Now,
        Title = "Net Conf Kuopio",
        Description = "Net Conf community event in Kuopio"
    }
};

app.MapGet("/items", async () => items);

app.Run();


public class ToDoItem
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}

