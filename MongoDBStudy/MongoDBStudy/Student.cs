using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBStudy
{
    [BsonIgnoreExtraElements]
    public class Student
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public DateTime Birthday { get; set; }


        public string[] Hobbies { get; set; }

        public List<Course> Courses { get; set; }

        //[BsonRepresentation(BsonType.ObjectId)]
        public string classroomid { get; set; }


        public bool IsDelete { get; set; }

        public Gender Gender { get; set; }

        public int Height { get; set; }
    }

    public class Course
    {
        public string CourseId { get; set; }
        public string Name { get; set; }

        public int Count { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
