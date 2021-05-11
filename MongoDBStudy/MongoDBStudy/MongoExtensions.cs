using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDBStudy.DbContext;

namespace MongoDBStudy
{

    public static class MongoExtensions
    {
        public static BsonDocument RenderToBsonDocument<T>(this FilterDefinition<T> filter)
        {
            var serializerRegistry = BsonSerializer.SerializerRegistry;
            var documentSerializer = serializerRegistry.GetSerializer<T>();
            return filter.Render(documentSerializer, serializerRegistry);
        }

        public static BsonDocument RenderToBsonDocument<T>(this ProjectionDefinition<T> filter)
        {
            var serializerRegistry = BsonSerializer.SerializerRegistry;
            var documentSerializer = serializerRegistry.GetSerializer<T>();
            return filter.Render(documentSerializer, serializerRegistry);
        }

        public static BsonDocument RenderToBsonDocument<T>(this UpdateDefinition<T> filter)
        {
            var serializerRegistry = BsonSerializer.SerializerRegistry;
            var documentSerializer = serializerRegistry.GetSerializer<T>();
            return (BsonDocument)filter.Render(documentSerializer, serializerRegistry);
        }


        public static BsonDocument RenderToBsonDocument<T, TOut>(this ProjectionDefinition<T, TOut> filter)
        {
            var serializerRegistry = BsonSerializer.SerializerRegistry;
            var documentSerializer = serializerRegistry.GetSerializer<T>();
            return filter.Render(documentSerializer, serializerRegistry).ToBsonDocument();
        }
    }
}
