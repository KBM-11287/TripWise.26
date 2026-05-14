using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace TripWise.Api.Models
{
    public class Destination
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("destinationName")]
        public string DestinationName { get; set; }

        [BsonElement("coordinates")]
        public GeoJsonPoint Coordinates { get; set; }
    }

    public class GeoJsonPoint
    {
        [BsonElement("type")]
        public string Type { get; set; } = "Point";
        
        [BsonElement("coordinates")]
        public double[] Coordinates { get; set; } // [longitude, latitude]
    }
}
