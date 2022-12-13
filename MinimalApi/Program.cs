using Microsoft.EntityFrameworkCore;
using MinimalApi.Data;
using MinimalApi.Models;
using MiniValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Db in memory
builder.Services.AddDbContext<TodoDb>(
    opt => opt.UseInMemoryDatabase("TodoList")
);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// Routes
app.MapGet("/", () => "Hello World");

app.MapGet("/todo", async ( TodoDb db) =>
{
    return Results.Ok( await db.Todos.ToListAsync());
});

app.MapGet("/todo/{id}", async (int id, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);
    Console.WriteLine(todo);
        if(todo == null)
            return Results.NotFound();
        return Results.Ok(todo);
});

app.MapPost("/todo", async (Todo todo, TodoDb db) =>
{
    if (!MiniValidator.TryValidate(todo, out var error))
        return Results.ValidationProblem(error);

    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todo/{todo.Id}", todo);

});

app.MapPut("/todo/{id}", async (int id, Todo todo, TodoDb db) =>
{
    var todoDb = await db.Todos.FindAsync(id);

    if(todoDb == null)
        return Results.NotFound();

    todoDb.Name = todo.Name;
    todoDb.IsComplete = todo.IsComplete;

    db.Todos.Update(todoDb);

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/todo/{id}", async (int id, TodoDb db) => 
{
    if(await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});


app.Run();

public partial class Program { }