namespace Todo.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        public string? Task { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}
