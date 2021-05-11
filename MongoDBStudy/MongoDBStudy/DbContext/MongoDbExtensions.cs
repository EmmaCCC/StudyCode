using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Linq;
namespace MongoDBStudy.DbContext
{
    public static class MongoDbExtensions
    {
        public static IFindFluent<T, T> Page<T>(this IFindFluent<T, T> findFluent, int pageIndex, int pageSize)
        {
            pageSize = Math.Min(30, pageSize);
            return findFluent.Skip((pageIndex - 1) * pageSize).Limit(pageSize);
        }

        public static IFindFluent<TDocument, TNewProjection> Project<TDocument, TNewProjection>(this IFindFluent<TDocument, TDocument> findFluent) where TNewProjection : class, new()
        {
            return findFluent.Project(ExpressionMapper.MapExp<TDocument, TNewProjection>());
        }

        public static FilterDefinition<TDocument> WhereIfExist<TDocument>(this FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, bool>> expression, object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return filter;
            }
            var builder = Builders<TDocument>.Filter;
            return filter & builder.Where(expression);
        }

        public static FilterDefinition<TDocument> WhereIf<TDocument>(this FilterDefinition<TDocument> filter,
            Expression<Func<TDocument, bool>> expression, bool condition)
        {
            if (condition)
            {
                var builder = Builders<TDocument>.Filter;
                return filter & builder.Where(expression);
            }
            return filter;
        }

        public static async Task<T> FindById<T>(this IMongoCollection<T> collection, string id)
        {
            var props = typeof(T).GetProperties();
            var idPropertyInfo = props.FirstOrDefault(a => a.GetCustomAttributes(typeof(BsonIdAttribute), true).Any());
            if (idPropertyInfo == null)
            {
                throw new InvalidOperationException("must have a primary key property");
            }
            //生成p.Id == id 表达式
            var argParam = Expression.Parameter(typeof(T), "p");
            var p1Exp = Expression.Property(argParam, idPropertyInfo.Name);
            var eqExp = Expression.Equal(p1Exp, Expression.Constant(id));

            //生成p.Id == id  && p.IsDelete == false 表达式
            var deletePropertyInfo = props.FirstOrDefault(a => a.Name == "IsDelete");
            if (deletePropertyInfo != null)
            {
                var p2Exp = Expression.Property(argParam, deletePropertyInfo.Name);
                var eq2Exp = Expression.Equal(p2Exp, Expression.Constant(false));
                eqExp = Expression.AndAlso(eqExp, eq2Exp);
            }

            var lambda = Expression.Lambda<Func<T, bool>>(eqExp, argParam);
            var json = collection.Find(lambda).ToString();
            return await collection.Find(lambda).FirstOrDefaultAsync();
        }
    }
}
