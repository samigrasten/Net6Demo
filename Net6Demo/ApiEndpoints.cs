namespace Net6Demo;

public static class ApiEndpoints
{
    private static readonly List<ToDoItem> _items = new List<ToDoItem> {
            new ToDoItem()
            {
                Id = 1,
                Date = DateTime.Now,
                Title = "Net Conf Kuopio",
                Description = "Net Conf community event in Kuopio"
            }
        };

    public static IEndpointRouteBuilder MapToDoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", (LinkGenerator linker) => $"Fetch all items from {linker.GetPathByName("Get all items", values: null)}")
            .ExcludeFromDescription();

        app.MapGet("/items", async () => _items)
            .Produces<List<ToDoItem>>(StatusCodes.Status200OK)
            .WithName("Get all items")
            .WithGroupName("v1")
            .WithTags("Queries");

        app.MapPost("/items/", async ([FromBody] ToDoItem item, HttpResponse response) =>
        {
            _items.Add(item);

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
            return _items.FirstOrDefault(item => item.Id == id) is ToDoItem item
                ? Results.Ok(item)
                : Results.NotFound();
        })
            .Produces<List<ToDoItem>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("Get item by id")
            .WithGroupName("v1")
            .WithTags("Queries");

        app.MapGet("/items/search/{filter}", async ([FromBody] SearchFilter filter) =>
        {
            if (filter == null) return Results.BadRequest();

            return _items.FirstOrDefault(item =>
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

        return app;
    }
}