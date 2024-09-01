using System.Text.Json.Serialization;

namespace Todo.Models.DomainModels
{
    public class TodoModel
    {
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [JsonPropertyName("task")]
        public string? Task { get; set; }

        [JsonPropertyName("isComplete")]
        public bool IsComplete { get; set; } = false;
    }
}
