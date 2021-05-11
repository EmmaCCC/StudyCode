using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace MongoDBStudy
{
    public class StringObjectIdRepresentationConvention : ConventionBase, IPostProcessingConvention
    {
        public void PostProcess(BsonClassMap classMap)
        {
            //以id结尾的字符串属性统一使用ObjectId生成
            foreach (var memberMap in classMap.DeclaredMemberMaps)
            {
                if (memberMap.MemberName.EndsWith("id", StringComparison.OrdinalIgnoreCase) &&
                    memberMap.MemberType == typeof(string))
                {
                    memberMap.SetIdGenerator(StringObjectIdGenerator.Instance).SetSerializer(new StringSerializer(BsonType.ObjectId));
                }
            }

        }
    }
}
