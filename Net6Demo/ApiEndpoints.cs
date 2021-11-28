using Microsoft.EntityFrameworkCore;


namespace Net6Demo;

public static class ApiEndpoints
{
    public static IEndpointRouteBuilder MapToDoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", (LinkGenerator linker) => $"Fetch all items from {linker.GetPathByName("Get all items", values: null)}")
            .ExcludeFromDescription();        

        app.MapGet("/items", async ([FromServices] ToDoDb db) => await db.ToDoItems.ToListAsync())
            .Produces<List<ToDoItem>>(StatusCodes.Status200OK)
            .WithName("Get all items")
            .WithGroupName("v1")
            .WithTags("Queries");

        app.MapPost("/items/", async ([FromBody] ToDoItem item, [FromServices] ToDoDb db, HttpResponse response) =>
        {
            db.ToDoItems.Add(item);
            await db.SaveChangesAsync();

            response.StatusCode = 201;
            response.Headers.Location = $"/todoItems/{item.Id}";
        })
            .Accepts<ToDoItem>("application/json")
            .Produces<ToDoItem>(StatusCodes.Status201Created)
            .WithName("Save item to todolist")
            .WithGroupName("v1")
            .WithTags("Commands");

        app.MapGet("/items/{id}", async (int id, [FromServices] ToDoDb db) =>
        {            
            return await db.ToDoItems.FirstOrDefaultAsync(item => item.Id == id) is ToDoItem item
                ? Results.Ok(item)
                : Results.NotFound();
        })
            .Produces<List<ToDoItem>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("Get item by id")
            .WithGroupName("v1")
            .WithTags("Queries");

        app.MapGet("/items/search/{filter}", async ([FromBody] SearchFilter filter, [FromServices] ToDoDb db) =>
        {
            if (filter == null) return Results.BadRequest();

            return await db.ToDoItems.FirstOrDefaultAsync(item =>
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

        app.MapGet("/breakpoint", () => BreakpointDemo.Start())
            .ExcludeFromDescription();

        return app;
    }
}