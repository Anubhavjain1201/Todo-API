using System.Text.Json.Serialization;

namespace Todo.Models
{
    public class DynamoDBObject
    {
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }

        [JsonPropertyName("Pk")]
        public string PrimaryKey => Id.ToString();

        [JsonPropertyName("Sk")]
        public string SortKey => Id.ToString();

        [JsonPropertyName("Task")]
        public string? Task {  get; set; }

        [JsonPropertyName("IsComplete")]
        public bool IsComplete { get; set; }
    }
}
