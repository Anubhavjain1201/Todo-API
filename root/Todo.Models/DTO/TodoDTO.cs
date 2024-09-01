using System.Text.Json.Serialization;

namespace Todo.Models.DTO
{
    public class TodoDTO
    {
        public Guid? Id { get; set; }

        public string? Task { get; set; }

        public bool IsComplete { get; set; } = false;
    }
}
