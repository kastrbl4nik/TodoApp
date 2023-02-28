namespace TodoApp.Domain.Models
{
    public class TodoListModel
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public bool? Hidden { get; set; }

        public DateTime? CreatedDate { get; set; }

        public TodoListModel(int? id, string title, string? description, bool? hidden, DateTime? createdDate)
        {
            Id = id;
            Title = title;
            Description = description;
            Hidden = hidden;
            CreatedDate = createdDate;
        }
    }
}
