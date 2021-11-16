var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("v1/todos", (AppDbContext context) =>
{
    var todos = context.Todos.ToList();
    return Results.Ok(todos);
}).Produces<List<Todo>>();

app.MapGet("v1/todos/{id}", (AppDbContext context, Guid id) =>
{
    var todo = context.Todos.SingleOrDefault(x => x.Id == id);
    return Results.Ok(todo);

}).Produces<Todo>();

app.MapPost("v1/todos", (AppDbContext context, CreateTodoViewModel model) =>
{
    var todo = new Todo(Guid.NewGuid(), model.Title, false);

    context.Todos.Add(todo);
    context.SaveChanges();

    return Results.Created($"/v1/todos/{todo.Id}", todo);
}).Produces<Todo>();

app.MapPut("v1/todos/{id}/changeTitle", (AppDbContext context, CreateTodoViewModel model, Guid id) =>
{
    var todo = context.Todos.SingleOrDefault(x => x.Id == id);

    todo.ChangeTitle(model.Title);

    context.Todos.Update(todo);
    context.SaveChanges();

    return Results.Accepted($"/v1/todos/{todo.Id}", todo);

}).Produces<Todo>();

app.MapPost("v1/todos/{id}/markAsDone", (AppDbContext context, Guid id) =>
{
    var todo = context.Todos.SingleOrDefault(x => x.Id == id);

    todo.MarkAsDone();

    context.Todos.Update(todo);
    context.SaveChanges();

    return Results.Accepted($"/v1/todos/{todo.Id}", todo);
}).Produces<Todo>();

app.Run();
