using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics;

namespace TripWise.Api.Models
{
    public class Trip
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("creatorId")]
        public string CreatorId { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("transportation")]
        public string Transportation { get; set; }

        [BsonElement("destination")]
        public string Destination { get; set; }

        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }

        [BsonElement("activities")]
        public List<Activity> Activities { get; set; } = new();
    }
}

    
