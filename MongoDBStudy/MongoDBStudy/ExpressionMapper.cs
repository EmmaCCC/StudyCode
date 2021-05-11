using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MongoDBStudy
{
    class ExpressionMapper
    {
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

            var props = typeof(TResult).GetProperties();
            var sourceProps = typeof(TSource).GetProperties();
            var sourceParam = Expression.Parameter(typeof(TSource), "p");

            var newExp = Expression.New(typeof(TResult));

            List<MemberBinding> memberBindings = new List<MemberBinding>();
            foreach (var p in props)
            {
                //如果source类型存在目标类型的属性才添加
                if (sourceProps.Any(a => a.Name == p.Name))
                {
                    var propertyExp = Expression.PropertyOrField(sourceParam, p.Name);
                    memberBindings.Add(Expression.Bind(typeof(TResult).GetMember(p.Name)[0], propertyExp));
                }
            }
            var body = Expression.MemberInit(newExp, memberBindings);
            var exp = Expression.Lambda<Func<TSource, TResult>>(body, sourceParam);
            return exp;

        }

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

    }
}
