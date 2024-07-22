using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ChatsMigration.Models
{
    public class MessageMongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdationDate { get; set; }
        public string MessageId { get; set; }
        public string Object { get; set; }
        public string ThreadId { get; set; }
        public string Role { get; set; }
        public List<Content> Content { get; set; } = new List<Content>();
        public List<string> FileIds { get; set; }
        public string AssistantId { get; set; }
        public string RunId { get; set; }
        public int UserId { get; set; }
        public int? CancelledAt { get; set; }
        public int? StartedAt { get; set; }
        public int? ExpiresAt { get; set; }
        public int CreatedAt { get; set; }
        public int? FailedAt { get; set; }
        public int? CompletedAt { get; set; }
        public bool IsActive { get; set; } = true;

        public static string GetName() => nameof(MessageMongoEntity);

    }

    public class Content
    {
        public MessageContentType Type { get; set; }
        public Text? Text { get; set; }
        public File? ImageFile { get; set; }
        public File? FileCitation { get; set; }
        public File? FilePath { get; set; }

    }

    public class File
    {
        public string FileId { get; set; }
    }

    public class Text
    {
        public string Value { get; set; }
        public IList<string> Annotations { get; set; }
    }
}
