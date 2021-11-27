using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.MapGet("/", (LinkGenerator linker) => $"Fetch all items from {linker.GetPathByName("Get all items", values: null)}")
    .ExcludeFromDescription(); 

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

app.MapGet("/items", async () => items)
    .Produces<List<ToDoItem>>(StatusCodes.Status200OK)
    .WithName("Get all items")
    .WithGroupName("v1")
    .WithTags("Queries");

app.MapPost("/items/", async ([FromBody] ToDoItem item, HttpResponse response) =>
{
    items.Add(item);
    
    response.StatusCode = 201;
    response.Headers.Location = $"/todoItems/{item.Id}";
})
    .Accepts<ToDoItem>("application/json")
    .Produces<ToDoItem>(StatusCodes.Status201Created)
    .WithName("Save item to todolist")
    .WithGroupName("v1")
    .WithTags("Commands");

app.MapGet("/items/{id}", async (int id) =>
{
    return items.FirstOrDefault(item => item.Id == id) is ToDoItem item
        ? Results.Ok(item)
        : Results.NotFound();
})
    .Produces<List<ToDoItem>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("Get item by id")
    .WithGroupName("v1")
    .WithTags("Queries");

app.MapGet("/items/search/{filter}", async ([FromBody]SearchFilter filter) =>
{
    if (filter == null) return Results.BadRequest();

    return items.FirstOrDefault(item =>
        item.Date.Day == filter.Date.Day
        && item.Date.Month == filter.Date.Month
        && item.Date.Year == filter.Date.Year
        && (item.Title.Contains(filter.Title, StringComparison.OrdinalIgnoreCase) 
            || item.Description.Contains(filter.Title, StringComparison.OrdinalIgnoreCase))) is ToDoItem item
        ? Results.Ok(item)
        : Results.NotFound();
})
    .Produces<List<ToDoItem>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("Get item by search criteria")
    .WithGroupName("v1")
    .WithTags("Queries");

app.MapMethods("/items/{*rest}", new[] { "DELETE", "PUT" }, () => Results.StatusCode(405))
    .ExcludeFromDescription(); 

app.Run();


