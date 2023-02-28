namespace TodoApp.Domain.Models
{
    public class TodoModel
    {
        public TodoModel(int? id, string? title, DateTime? dueDate, bool completed)
        {
            Id = id;
            Title = title;
            DueDate = dueDate;
            Completed = completed;
        }

        public int? Id { get; set; }

        public string? Title { get; set; }

        public DateTime? DueDate { get; set; }

        public bool Completed { get; set; }
    }
}
