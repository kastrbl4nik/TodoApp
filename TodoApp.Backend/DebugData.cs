using TodoApp.Domain;
using TodoApp.Domain.Entities;

namespace TodoApp.Backend
{
    public static class DebugData
    {
        public static void EnsurePopulated(IApplicationBuilder app) 
        {
            TodoAppDbContext context = app.ApplicationServices
                        .CreateScope().ServiceProvider.GetRequiredService<TodoAppDbContext>();
            if(!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Username = "kastrbl4nik",
                        TodoLists = new List<TodoList>()
                        {
                            new TodoList
                            {
                                Title = "Work list",
                                Description = "The things i got to do before work",
                                Todos = new List<Todo>
                                {
                                    new Todo
                                    {
                                        Title = "Get up",
                                        Description = "Lorem ipsum dolor sit amet",
                                        DueDate = DateTime.Now,
                                    },
                                    new Todo
                                    {
                                        Title = "Do stuff",
                                        Description = "Lorem ipsum dolor sit amet",
                                        DueDate = DateTime.Now,
                                    },
                                    new Todo
                                    {
                                        Title = "Do some more stuff",
                                        Description = "Lorem ipsum dolor sit amet",
                                        DueDate = DateTime.Now,
                                    }
                                }
                            },
                            new TodoList
                            {
                                Title = "Some other list",
                                Description = "The things i got to do no matter what",
                                Todos = new List<Todo>
                                {
                                    new Todo
                                    {
                                        Title = "Do some really important stuff",
                                        Description = "Lorem ipsum dolor sit amet",
                                        DueDate = DateTime.Now,
                                    },
                                    new Todo
                                    {
                                        Title = "Bing Bong",
                                        Description = "Lorem ipsum dolor sit amet",
                                        DueDate = DateTime.Now,
                                    }
                                }
                            },
                        },
                    },
                    new User
                    {
                        Username = "user1234",
                        TodoLists = new List<TodoList>()
                        {
                            new TodoList
                            {
                                Title = "Work list",
                                Description = "The things i got to do before work",
                                Todos = new List<Todo>
                                {
                                    new Todo
                                    {
                                        Title = "Get up",
                                        Description = "Lorem ipsum dolor sit amet",
                                        DueDate = DateTime.Now,
                                    },
                                    new Todo
                                    {
                                        Title = "Do stuff",
                                        Description = "Lorem ipsum dolor sit amet",
                                        DueDate = DateTime.Now,
                                    },
                                    new Todo
                                    {
                                        Title = "Do some more stuff",
                                        Description = "Lorem ipsum dolor sit amet",
                                        DueDate = DateTime.Now,
                                    }
                                }
                            },
                            new TodoList
                            {
                                Title = "Some other list",
                                Description = "The things i got to do no matter what",
                                Todos = new List<Todo>
                                {
                                    new Todo
                                    {
                                        Title = "Do some really important stuff",
                                        Description = "Lorem ipsum dolor sit amet",
                                        DueDate = DateTime.Now,
                                    },
                                    new Todo
                                    {
                                        Title = "Bing Bong",
                                        Description = "Lorem ipsum dolor sit amet",
                                        DueDate = DateTime.Now,
                                    }
                                }
                            },
                        },
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
