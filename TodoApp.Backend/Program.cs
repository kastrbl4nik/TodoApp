using Microsoft.EntityFrameworkCore;
using TodoApp.Domain;
using TodoApp.Domain.Repositories;
using TodoApp.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoAppDbContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:TodoAppConnection"]);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITodoListRepository, TodoListRepository>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TodoListService>();
builder.Services.AddScoped<TodoService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    /*using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<TodoAppDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }*/
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
