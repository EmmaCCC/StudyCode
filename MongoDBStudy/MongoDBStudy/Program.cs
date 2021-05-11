using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDBStudy.DbContext;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace MongoDBStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.MongoExample.CRUD();

            MongoExample.Convention();
            //MongoExample.Concurrent();

            Console.WriteLine("执行成功");
            Console.ReadKey();
        }



    }

}
