using System.Collections.Generic;
using MongoDB.Driver;

namespace MongoDBStudy.DbContext
{
    public class SchoolDbContext
    {
        public static MongoClient MongoClient { get; }
        public static IMongoDatabase Database { get; }

        static SchoolDbContext()
        {
            MongoClient = new MongoClient("mongodb://localhost:27017");
            Database = MongoClient.GetDatabase("school");
        }

        private static IMongoCollection<T> Collection<T>()
        {
            return Database.GetCollection<T>(typeof(T).Name);
        }

        public static IMongoCollection<Student> Students => Collection<Student>();
    }
}
