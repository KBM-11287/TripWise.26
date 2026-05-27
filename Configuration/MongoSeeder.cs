using MongoDB.Driver;
using System.Collections.Generic;
using TripWise.Api.Models;

namespace TripWise.Api.Config
{
    public class MongoSeeder
    {
        private readonly IMongoCollection<User> _userCollection;

        public MongoSeeder(IMongoDatabase database)
        {
            _userCollection = database.GetCollection<User>("Users");
        }

        public async Task SeedAsync()
        {

            // check if data already exists
            //var existingUsers = await _userCollection.CountDocumentsAsync(_ => true);
            //if (existingUsers == 0)
            //{

                var users = new List<User>
            {
                new User { Name = "Admin", Email = "admin@example.com" },
                new User { Name = "Test User", Email = "test@example.com" }
            };
                await _userCollection.InsertManyAsync(users);
            Console.WriteLine("created USERS");
            //}
        }
    }
}
