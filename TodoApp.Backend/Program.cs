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

builder.Services.AddScoped<ITodoRepository, EfTodoRepository>();
builder.Services.AddScoped<TodoService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
