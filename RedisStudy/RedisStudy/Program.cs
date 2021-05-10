using System;
using System.Collections.Generic;

namespace RedisStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var redis = new CSRedis.CSRedisClient("192.168.50.71:6379");
            RedisHelper.Initialization(redis);


            #region set操作
            RedisHelper.SAdd("top", "123", "456", "123");
            RedisHelper.SAdd("top2", "asd");

            
            #endregion

            #region 字符串操作
            RedisHelper.Append("str1", "hello");
            #endregion

            #region Hash操作
            RedisHelper.HSet("person", "age", 1);
            RedisHelper.HSet("person", "name", "zhangsan");
            RedisHelper.HSet("person", "address", new { address = "henan", city = "nanyang" });

           

            RedisHelper.HMSet("shop", "name", "songlin", "scale", 500, "address", "henan");

            var person = RedisHelper.HGet<dynamic>("person", "address");

            var shop = RedisHelper.HMGet("shop", "name", "address");

            //RedisHelper.HDel("person", "age", "name");
            #endregion

            #region list操作

            RedisHelper.LPush("list", "value1", "value2", "value3");

            #endregion

            #region 有序集合
            RedisHelper.ZAdd("zset", (1.6m, new Person { Name = "songlin" }), (2.3m, new Person { Name = "wangwu" }), (1.67m, new Person { Name = "lisi" }));
            #endregion

            RedisHelper.Expire("list", 15);



            Console.WriteLine("success");
            Console.ReadLine();
        }
    }


    class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }
    }
}
