

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Net6Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

app.MapPost("/items/", async ([FromBody] ToDoItem item, HttpResponse response) =>
{
    items.Add(item);
    
    response.StatusCode = 201;
    response.Headers.Location = $"/todoItems/{item.Id}";
});

app.MapGet("/items/{id}", async (int id) =>
{
    return items.FirstOrDefault(item => item.Id == id) is ToDoItem item
        ? Results.Ok(item)
        : Results.NotFound();
});

app.Run();


