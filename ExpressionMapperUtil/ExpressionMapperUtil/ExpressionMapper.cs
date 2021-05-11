using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionMapperUtil
{
    public class ExpressionMapper
    {
        private static ConcurrentDictionary<Type, PropertyInfo[]> _types = new ConcurrentDictionary<Type, PropertyInfo[]>();

        private static PropertyInfo[] GetProperties(Type type)
        {
            var props = _types.GetOrAdd(type, type.GetProperties());
            return props;
        }

        /// <summary>
        /// 生成lambda表达式，类似：
        /// p => new TResult(){
        ///  xxx = p.xxx,
        ///  yyy= p.yyy
        /// }
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static Expression<Func<TSource, TResult>> MapExp<TSource, TResult>() where TResult : class, new()
        {

            var props = GetProperties(typeof(TResult));
            var sourceProps = GetProperties(typeof(TSource));

            var sourceParam = Expression.Parameter(typeof(TSource), "p");

            var newExp = Expression.New(typeof(TResult));

            List<MemberBinding> memberBindings = new List<MemberBinding>();
            foreach (var p in props)
            {
                var resultPropertyName = p.Name;
                var mapProperty = (MapSourcePropertyAttribute)p.GetCustomAttribute(typeof(MapSourcePropertyAttribute));
                if (mapProperty != null && !string.IsNullOrEmpty(mapProperty.Name))
                {
                    resultPropertyName = mapProperty.Name;
                }

                //如果source类型存在结果类型的属性名才添加，以结果类型的属性为基准
                if (sourceProps.Any(a => a.Name == resultPropertyName))
                {
                    var propertyExp = Expression.PropertyOrField(sourceParam, resultPropertyName);
                    memberBindings.Add(Expression.Bind(typeof(TResult).GetMember(p.Name)[0], propertyExp));
                }
            }
            var body = Expression.MemberInit(newExp, memberBindings);
            var exp = Expression.Lambda<Func<TSource, TResult>>(body, sourceParam);
            return exp;

        }

        /// <summary>
        /// 可以添加额外的表达式属性值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="binding"></param>
        /// <returns></returns>
        public static Expression<Func<TSource, TResult>> MapExp<TSource, TResult>(MemberBinding binding) where TResult : class, new()
        {

            var props = typeof(TResult).GetProperties();

            var sourceParam = Expression.Parameter(typeof(TSource), "p");

            var newExp = Expression.New(typeof(TResult));

            List<MemberBinding> memberBindings = new List<MemberBinding>();
            foreach (var p in props)
            {
                var propertyExp = Expression.PropertyOrField(sourceParam, p.Name);
                memberBindings.Add(Expression.Bind(typeof(TResult).GetMember(p.Name)[0], propertyExp));
            }
            if (binding != null)
            {
                memberBindings.Add(binding);
            }
            var body = Expression.MemberInit(newExp, memberBindings);
            var exp = Expression.Lambda<Func<TSource, TResult>>(body, sourceParam);
            return exp;

        }

        public static TResult Map<TSource, TResult>(TSource source) where TResult : class, new()
        {
            return MapExp<TSource, TResult>().Compile().Invoke(source);
        }
    }

    public class MapSourcePropertyAttribute : Attribute
    {
        public string Name { get; set; }
        public MapSourcePropertyAttribute(string name)
        {
            this.Name = name;
        }
    }
}
