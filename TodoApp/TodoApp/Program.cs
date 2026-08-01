using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoContext>(options => { 
    options.UseInMemoryDatabase("TodoList"); 
});
var app = builder.Build();

app.MapGet("/todos", async  (TodoContext db) =>
{
    return await db.todos.ToListAsync();
});
app.MapGet("/todos/{id}", async (Guid id,TodoContext db) =>
{
    return await db.todos.FindAsync(id);
});
app.MapPost("/todos", async (Todo todo, TodoContext db) =>
{
     await db.todos.AddAsync(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{todo.Id}",todo);
});
app.MapPut("/todos/{id}", async (Guid id,Todo todo, TodoContext db) =>
{
    var entity = await db.todos.FindAsync(id);
    if (entity is null) return Results.NotFound();
    entity!.Name = todo.Name;
    entity.Completed = todo.Completed;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/todos", async (Guid id, TodoContext db) =>
{
    var entity = await db.todos.FindAsync(id);
    if (entity is null) return Results.NotFound();
    db.Remove(entity);
    await db.SaveChangesAsync();
    return Results.NotFound();
});

app.Run();
