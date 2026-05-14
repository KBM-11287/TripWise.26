using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TripWise.Api.Models
{
    public class Activity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }
        
        [BsonElement("description")]
        public string Description { get; set; }
        
        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("startTime")]
        public DateTime StartTime { get; set; }

        [BsonElement("endTime")]
        public DateTime EndTime { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }
    }
}
