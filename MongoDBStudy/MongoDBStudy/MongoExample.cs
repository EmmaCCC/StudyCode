using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDBStudy.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBStudy
{
    class MongoExample
    {
        public static void CRUD()
        {
            var exp = ExpressionMapper.MapExp<StudentVm, Student>();

            #region 连接Mongodb
            var client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("school");

            #endregion

            #region 约束 驼峰命名法

            var pack = new ConventionPack();
            pack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register("CamelCaseElementNameConvention", pack, t => true);

            #endregion

            #region 1.插入数据

            var collection = database.GetCollection<Student>("student");
            if (collection.CountDocuments(a => true) == 0)
            {
                collection.InsertOne(new Student()
                {
                    Age = 27,
                    Name = "zhangsan",
                    Address = "河南",
                    Birthday = DateTime.Parse("1992-06-17 12:35:12"),
                    Hobbies = new[] { "阅读", "跑步", },
                    Courses = new List<Course>()
                    {
                        new Course()
                        {
                             Name = "计算机理论",
                             Count = 30
                        },
                        new Course()
                        {
                            Name = "操作系统",
                            Count = 35
                        }
                    }

                });

                collection.InsertOne(new Student()
                {
                    Age = 23,
                    Name = "lisi",
                    Address = "北京",
                    Birthday = DateTime.Parse("1996-05-17 14:40:08"),
                    Hobbies = new[] { "游戏", "睡觉", "代码" },
                    Courses = new List<Course>()
                    {
                        new Course()
                        {
                             Name = "c#语言基础",
                             Count = 30
                        },
                        new Course()
                        {
                            Name = "编译原理",
                            Count = 35
                        }
                    }
                });

                var bulkList = new List<Student>()
                {

                    new Student()
                    {
                        Age = 26,
                        Name = "wangwu",
                        Address = "河北",
                        Birthday = DateTime.Parse("1993-08-17 16:40:08"),
                        Hobbies = new[] { "睡觉" },
                        Courses = new List<Course>()
                            {
                                new Course()
                                {
                                     Name = "网络编程",
                                     Count = 30
                                },
                                new Course()
                                {
                                    Name = "操作系统",
                                    Count = 35
                                }
                            }
                    },
                    new Student()
                        {
                            Age = 29,
                            Name = "wangwu",
                            Address = "南京",
                            Birthday = DateTime.Parse("1990-05-17 14:40:08"),
                            Hobbies = new[] { "游戏", "睡觉", "代码", "逛街" },
                            Courses = new List<Course>()
                                {
                                    new Course()
                                    {
                                         Name = "网络编程",
                                         Count = 30
                                    },
                                    new Course()
                                    {
                                        Name = "操作系统",
                                        Count = 35
                                    }
                                }
                        }

                };

                collection.InsertMany(bulkList);
            }



            #endregion

            Console.WriteLine();

            #region 2.过滤器(Filter)
            Console.WriteLine("==============过滤==============");

            //空过滤器匹配全部
            FilterDefinition<BsonDocument> filter0 = Builders<BsonDocument>.Filter.Empty;

            //一般匹配
            FilterDefinition<BsonDocument> filter1 = "{name:'zhangsan'}";
            FilterDefinition<BsonDocument> filter2 = new BsonDocument("name", "zhangsan");

            var builder3 = Builders<BsonDocument>.Filter;
            var filter3 = builder3.Eq("name", "zhangsan") & builder3.Gt("age", 10);


            var builder4 = Builders<Student>.Filter;
            var filter4 = (builder4.Eq("name", "zhangsan") & builder4.Gt(a => a.Age, 10)) | builder4.Gt(a => a.Birthday, DateTime.Parse("1990-01-01"));



            var query21 = database.GetCollection<BsonDocument>("student").Find(filter1);
            var json21 = query21.ToString();
            var student21 = query21.FirstOrDefault();
            Console.WriteLine(json21);

            var query22 = database.GetCollection<BsonDocument>("student").Find(filter2);
            var json22 = query22.ToString();
            var student22 = query22.FirstOrDefault();
            Console.WriteLine(json22);


            var query23 = database.GetCollection<BsonDocument>("student").Find(filter3);
            var json23 = query23.ToString();
            var student23 = query23.FirstOrDefault();
            Console.WriteLine(json23);


            var query24 = database.GetCollection<Student>("student").Find(filter4);
            var json24 = query24.ToString();
            var student24 = query24.FirstOrDefault();
            Console.WriteLine(json24);


            //数组匹配
            var builder5 = Builders<BsonDocument>.Filter;
            var filter25 = builder5.AnyEq("courses.name", "操作系统");

            var query25 = database.GetCollection<BsonDocument>("student").Find(filter25);
            var json25 = query25.ToString();
            var student25 = database.GetCollection<BsonDocument>("student").Find(filter25).ToList();
            Console.WriteLine(json25);


            var builder6 = Builders<Student>.Filter;
            var filter26 = builder6.ElemMatch(a => a.Courses, a => a.Name == "操作系统");
            var query26 = database.GetCollection<Student>("student").Find(filter26);
            var json26 = query26.ToString();
            var student26 = database.GetCollection<Student>("student").Find(filter26).ToList();
            Console.WriteLine(json26);


            var builder7 = Builders<Student>.Filter;
            var filter27 = builder7.ElemMatch(a => a.Courses, a => a.Name == "操作系统") & builder7.ElemMatch(a => a.Courses, a => a.Count > 30);
            var query27 = database.GetCollection<Student>("student").Find(filter27);
            var json27 = query27.ToString();
            var student27 = database.GetCollection<Student>("student").Find(filter27).ToList();
            Console.WriteLine(json27);



            var builder8 = Builders<Student>.Filter;
            var filter28 = builder8.ElemMatch(a => a.Courses, a => a.Name == "操作系统") | builder8.ElemMatch(a => a.Courses, a => a.Count > 30);
            var query28 = database.GetCollection<Student>("student").Find(filter28);
            var json28 = query28.ToString();
            var student28 = database.GetCollection<Student>("student").Find(filter28).ToList();
            Console.WriteLine(json28);



            var builder9 = Builders<Student>.Filter;
            var filter29 = builder9.SizeGte(a => a.Hobbies, 4);
            var query29 = database.GetCollection<Student>("student").Find(filter29);
            var json29 = query29.ToString();
            var student29 = database.GetCollection<Student>("student").Find(filter29).ToList();
            Console.WriteLine(json29);



            var builder10 = Builders<Student>.Filter;
            var filter210 = builder10.In(a => a.Name, new[] { "zhangsan", "wangwu" });
            var query210 = database.GetCollection<Student>("student").Find(filter210);
            var json210 = query210.ToString();
            var student210 = database.GetCollection<Student>("student").Find(filter210).ToList();
            Console.WriteLine(json210);



            var builder11 = Builders<Student>.Filter;
            var filter211 = builder11.AnyEq(a => a.Hobbies, "睡觉");
            var query211 = database.GetCollection<Student>("student").Find(filter211);
            var json211 = query211.ToString();
            var student211 = database.GetCollection<Student>("student").Find(filter211).ToList();
            Console.WriteLine(json211);


            var builder12 = Builders<Student>.Filter;
            var filter212 = builder12.Regex(a => a.Name, "ng");
            var query212 = database.GetCollection<Student>("student").Find(filter212);
            var json212 = query212.ToString();
            var student212 = database.GetCollection<Student>("student").Find(filter212).ToList();
            Console.WriteLine(json212);




            var indexes = database.GetCollection<Student>("student").Indexes;

            var indexKey = Builders<Student>.IndexKeys.Text(a => a.Name);
            var model = new CreateIndexModel<Student>(indexKey);
            indexes.CreateOne(model);

            var list = indexes.List().ToList();

            var builder13 = Builders<Student>.Filter;
            var filter213 = builder13.Text("zhangsan");
            var query213 = database.GetCollection<Student>("student").Find(filter213);
            var json213 = query213.ToString();
            var student213 = database.GetCollection<Student>("student").Find(filter213).ToList();
            Console.WriteLine(json213);

            Console.WriteLine("==============过滤==============");

            #endregion

            Console.WriteLine();

            #region 3.投影（Projections）

            Console.WriteLine("==============投影==============");

            ProjectionDefinition<BsonDocument> projection1 = "{age:1}";
            ProjectionDefinition<BsonDocument> projection2 = new BsonDocument("age", 1);

            var json31 = projection1.RenderToBsonDocument();

            var students31 = database.GetCollection<BsonDocument>("student").Find(a => true).Project(projection1).ToList();


            Console.WriteLine(json31);

            var projection3 = Builders<Student>.Projection.Include(a => a.Age).Include(a => a.Name);

            var projection4 = Builders<Student>.Projection.Exclude(a => a.Id).Exclude(a => a.Courses);

            var projection5 = Builders<Student>.Projection.Exclude(a => a.Id);

            var query33 = database.GetCollection<Student>("student").Find(a => true).Project<Student>(projection3);
            var json33 = query33.ToString();
            var students33 = query33.ToList();
            Console.WriteLine(json33);

            var query34 = database.GetCollection<Student>("student").Find(a => true).Project<Student>(projection4);
            var json34 = query34.ToString();
            var students34 = query34.ToList();
            Console.WriteLine(json34);

            var query35 = database.GetCollection<Student>("student").Find(a => true).Project<Student>(projection5);
            var json35 = query35.ToString();
            var students35 = query35.ToList();
            Console.WriteLine(json35);

            var projection6 = Builders<Student>.Projection.Expression(a => new StudentVm()
            {
                Age = a.Age,
                Id = a.Id,
                Name = a.Name
            });
            var query36 = database.GetCollection<Student>("student").Find(a => true).Project(projection6);
            var json36 = query36.ToString();
            var students36 = query36.ToList();
            Console.WriteLine(json36);


            var students37 = database.GetCollection<Student>("student").Find(a => true).Project(a => new StudentVm
            {
                Age = a.Age,
                Id = a.Id,
                Name = a.Name

            }).ToList();


            var query38 = database.GetCollection<Student>("student").Find(a => true).Project(a => new
            {
                a.Age,
                a.Id,
                a.Name

            });
            var json38 = query38.ToString();
            var students38 = query38.ToList();
            Console.WriteLine(json38);




            var query39 = database.GetCollection<Student>("student").Find(a => true).Project(a => new
            {
                AgeAndName = a.Name + "," + a.Age.ToString()
            });
            var json39 = query39.ToString();
            var students39 = query39.ToList();
            Console.WriteLine(json39);



            //自动投影 根据StudentVm中的属性自动生成
            var query310 = database.GetCollection<Student>("student").Find(a => true).Project(ExpressionMapper.MapExp<Student, StudentVm>());
            var json310 = query310.ToString();
            var listss = query310.ToList();
            Console.WriteLine($"自动投影:{json310}");

            Console.WriteLine("==============投影==============");

            #endregion

            Console.WriteLine();

            #region 4.排序(Sort)
            Console.WriteLine("==============排序==============");

            SortDefinition<BsonDocument> sort1 = "{ age: 1 }";

            SortDefinition<BsonDocument> sort2 = Builders<BsonDocument>.Sort.Ascending("_id").Descending("age");

            SortDefinition<Student> sort3 = Builders<Student>.Sort.Ascending(a => a.Id).Descending(a => a.Age);

            var query41 = database.GetCollection<Student>("student").Find(new BsonDocument()).Sort(sort3);
            var json41 = query41.ToString();
            var student41 = query41.ToList();
            Console.WriteLine(json41);
            Console.WriteLine("==============排序==============");

            #endregion

            Console.WriteLine();

            #region 5.更新(Update)

            Console.WriteLine("==============更新==============");


            UpdateDefinition<BsonDocument> update1 = "{ $set: { age: 25 } }";

            UpdateDefinition<BsonDocument> update2 = new BsonDocument("$set", new BsonDocument("age", 1));

            //数组更新添加
            var updateBuilder = Builders<Student>.Update;
            UpdateDefinition<Student> update3 = updateBuilder
                                            .Set(a => a.Name, "修改名称")
                                            .Set(a => a.Age, 25)
                                            .Push(a => a.Hobbies, "购物")
                                            .PushEach(a => a.Courses, new List<Course>()
                                            {
                                                new Course()
                                                {
                                                    Name = "马克思主义理论基础",
                                                    Count = 10,
                                                },
                                                new Course()
                                                {
                                                    Name = "毛泽东思想概论",
                                                    Count = 10,
                                                }
                                            })
                                            .CurrentDate(a => a.Birthday, UpdateDefinitionCurrentDateType.Date);

            var json53 = update3.RenderToBsonDocument();
            var student53 = collection.UpdateOne(a => a.Name == "zhangsan", update3);
            Console.WriteLine(json53);


            //数组更新移除
            UpdateDefinition<Student> update4 = updateBuilder
                                          .Set(a => a.Name, "zhangsan")
                                          .Inc(a => a.Age, 100)
                                          .Pull(a => a.Hobbies, "购物")
                                          .PullFilter(a => a.Courses, a => a.Name == "毛泽东思想概论");

            var json54 = update4.RenderToBsonDocument();
            var student54 = collection.UpdateOne(a => a.Name == "修改名称", update4);
            Console.WriteLine(json54);


            //匹配数组中得某个元素，并更新这个元素的相关字段
            UpdateDefinition<Student> update5 = updateBuilder
                                            .Set(a => a.Courses[-1].Name, "ssss");

            var json55 = update5.RenderToBsonDocument();
            var student55 = collection.UpdateOne(a => a.Courses.Any(b => b.Name == "马克思主义理论基础"), update5);
            Console.WriteLine(json55);



            UpdateDefinition<Student> update6 = updateBuilder
                                           .Mul(a => a.Age, 12);

            var op = new FindOneAndUpdateOptions<Student>()
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };

            var student56 = collection.FindOneAndUpdate<Student>(a => a.Age > 26, update6, op);



            var update7 = updateBuilder
                        .PopFirst(a => a.Hobbies)
                        .PopLast(a => a.Courses);
            var json57 = update7.RenderToBsonDocument();
            var student57 = collection.UpdateOne(a => a.Name == "zhangsan", update7);
            Console.WriteLine(json57);



            var update8 = updateBuilder
                            .SetOnInsert(a => a.Name, "ssss");

            var json58 = update8.RenderToBsonDocument();
            var student58 = collection.UpdateOne(a => a.Name == "ls", update8, new UpdateOptions() { IsUpsert = true });
            Console.WriteLine(json58);


            var update9 = updateBuilder
                           .Rename(a => a.Address, "homeAddress");

            var json59 = update9.RenderToBsonDocument();
            var student59 = collection.UpdateOne(a => a.Name == "wangwu", update9);
            Console.WriteLine(json59);


            var update10 = updateBuilder
                         .Set(a => a.Name, "**");

            var json510 = update10.RenderToBsonDocument();
            var student510 = collection.UpdateMany(a => a.Age > 20, update10);
            Console.WriteLine(json510);

            //根据条件动态更新
            var update11 = updateBuilder
                .Set(a => a.Name, "**");
            if (true)
            {
                update11 = update11.Set(a => a.Age, 12);
            }
            var json511 = update11.RenderToBsonDocument();
            Console.WriteLine(json511);

            Console.WriteLine("==============更新==============");
            #endregion

            Console.WriteLine();

            #region 6.聚合(Aggregation)


            Console.WriteLine("==============聚合==============");


            var pipeline1 = new BsonDocument[] {
                new BsonDocument{ { "$sort", new BsonDocument("_id", 1) }},
                new BsonDocument{{"$group", new BsonDocument{
                            {"_id", "$age"},
                            {"min",new BsonDocument{
                                    {"$min","$age"}}
                            }}
                    }}
                };

            var student61 = collection.Aggregate<BsonDocument>(pipeline1).ToListAsync();



            var agg = collection.Aggregate();

            var gg = agg
                 .Match(a => a.Age > 10)
                 .Project(a => new
                 {
                     a.Age,
                     a.Name
                 })
                 .Group(key => key.Age, g => new
                 {
                     age = g.Key,
                     count = g.Count(),
                     max = g.Max(a => a.Age),
                     min = g.Min(a => a.Age),
                     avg = g.Average(a => a.Age)
                 });


            var json62 = gg.ToString();
            var student62 = gg.ToList();
            Console.WriteLine(json62);

            Console.WriteLine("==============聚合==============");


            #endregion

            Console.WriteLine();

            #region 7.删除数据

            var student71 = collection.DeleteOne(a => a.Age > 100);

            var student72 = collection.DeleteMany(a => a.Age > 100);

            var op7 = new FindOneAndDeleteOptions<Student>()
            {

            };
            var student73 = collection.FindOneAndDelete<Student>(a => a.Age > 100, op7);


            #endregion

            Console.WriteLine();

            #region 8.快速使用

            Console.WriteLine("==============快速使用==============");

            var student80 = collection.Find(a => true).FirstOrDefault();

            var students81 = collection.Find(a => true).ToList();

            var students82 = collection.Find(a => true).Skip(80).Limit(20).ToList();

            var long83 = collection.Find(a => true).CountDocuments();

            var bool84 = collection.Find(a => true).Any();

            var students85 = collection.Find(a => true).SortBy(a => a.Id).SortByDescending(a => a.Age).ToList();


            var queryable = collection.AsQueryable();

            var queryable86 = queryable.Where(a => a.Age > 20).Select(a => new
            {
                a.Age,
                a.Name
            })
            .GroupBy(a => a.Age)
            .Select(g => new
            {
                g.Key,
                Max = g.Max(a => a.Age),
                Min = g.Min(a => a.Age),
                Avg = g.Average(a => a.Age),
                Name = g.First().Name,
                Count = g.Count(),
            });


            var json86 = queryable86.ToString();
            Console.WriteLine(json86);

            Console.WriteLine("==============快速使用==============");


            #endregion

            #region 9.执行命令

            //批量更新不同的值
            var dic = new Dictionary<string, object>()
            {
                {"update", "students"},
                {"updates",new List<BsonDocument>()}
            };

            //循环生成不同的更新命令
            for (int i = 0; i < 10; i++)
            {
                ((List<BsonDocument>)dic["updates"]).Add(new BsonDocument()
                {
                    {"q", new BsonDocument("age", i * 5)},
                    {
                        "u", new BsonDocument("$set",
                            new BsonDocument()
                            {
                                {"age", i + 2},
                                {"name", i + "-"}
                            }
                        )
                    },

                });
            }

            var result = database.RunCommand<BsonDocument>(new BsonDocument(dic));

            #endregion
            //批量操作
            #region 10.批量操作
            var write = new UpdateOneModel<Student>(filter211, update10);
            collection.BulkWrite(new List<UpdateOneModel<Student>>() { write });
            #endregion

            #region 11.DbContext封装
            var students = SchoolDbContext.Students.Find(a => true);

            using (var session = SchoolDbContext.MongoClient.StartSession())
            {
                session.StartTransaction();
                SchoolDbContext.Students.Find(session, a => true);
                session.CommitTransaction();

            }


            #endregion


            #region 12.测试

            WriteConcern writeConcern = new WriteConcern();

            var update12 = Builders<Student>.Update.Set(a => a.Age, 25);
            var result12 = database.GetCollection<Student>("student22").UpdateOne(a => a.Name == "ss", update12);

            #endregion

            #region 13.聚合2

            var p1 = PipelineStageDefinitionBuilder.Match<Student>(a => a.Name == "**");
            var student101 = collection.Aggregate().AppendStage(p1).ToList();

            #endregion

            #region 13.连接

            //参看:https://stackoverflow.com/questions/50530363/aggregate-lookup-with-c-sharp

            Console.WriteLine("==============连接查询==============");
            var studentQuery = database.GetCollection<Student>("student").AsQueryable();
            var classroomQuery = database.GetCollection<ClassRoom>("classroom").AsQueryable();

            //使用集合的方式来解决左连接
            var student102 = from s in studentQuery.Where(a => a.Name == "**")
                             join c in classroomQuery on s.classroomid equals c.Id into joined
                             select new
                             {
                                 s.Name,
                                 s.Age,
                                 classname = joined
                             };

            var query102 = student102.ToString();
            Console.WriteLine(query102);
            var result102 = student102.ToList();

            //database.GetCollection<Student>("student").Aggregate()
            //    .Lookup().ToList();


            #endregion

            #region 14 动态生成根据id查找

            var result141 = collection.FindById("5d8894a114294d2acc82ecd6").Result;
            #endregion

        }

        public static void Convention()
        {
            #region 连接Mongodb
            var client = new MongoClient("mongodb://localhost:27017");

            var database = client.GetDatabase("school");

            #endregion

            #region 约束 驼峰命名法

            var pack = new ConventionPack();
            //小驼峰名称法
            //pack.Add(new CamelCaseElementNameConvention());
            //stringid自动序列化为ObjectId
            pack.Add(new StringObjectIdRepresentationConvention());
            pack.Add(new IgnoreExtraElementsConvention(true));
            //枚举存为字符串
            pack.Add(new EnumRepresentationConvention(BsonType.String));
            ConventionRegistry.Register("pack", pack, t => true);

            //时间自动序列化为local时间
            BsonSerializer.RegisterSerializer(DateTimeSerializer.LocalInstance);

            database.DropCollection("student");
            database.GetCollection<Student>("student").InsertOne(new Student()
            {
                Birthday = DateTime.Now,
                classroomid = ObjectId.GenerateNewId().ToString(),
                Courses = new List<Course>()
                 {
                     new Course()
                     {
                          Count = 1,
                           Name = "马列",
                            CourseId =ObjectId.GenerateNewId().ToString()
                     }
                 }

            });
            var list = database.GetCollection<Student>("student").Find(new BsonDocument()).ToList();
            foreach (var item in list)
            {
                Console.WriteLine($"name:{item.Name},gender:{item.Gender}");
            }
            #endregion
        }

        public static void Concurrent()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("school");
            var colllection = database.GetCollection<Student>("student");
            var update = Builders<Student>.Update.Inc(a => a.Age, 1);
            for (int i = 0; i < 10; i++)
            {
                var index = i;
                colllection.UpdateOne(a => a.Id == "5e677f07e89a314f18d62b54", update);
                Console.WriteLine("执行成功" + index);
                //Task.Run(() =>
                //{
                //    colllection.UpdateOne(a => a.Id == "5e677f07e89a314f18d62b54", update);
                //    Console.WriteLine("执行成功" + index);
                //});
            }
        }
    }
}
