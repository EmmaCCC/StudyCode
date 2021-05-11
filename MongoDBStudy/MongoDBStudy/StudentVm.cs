﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBStudy
{
    [BsonIgnoreExtraElements]
    class StudentVm
    {

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }
     
    }

}
