using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;

namespace ESDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            var bb = (int) Math.Ceiling((decimal) 35 / 60);

            var client = HighLevelEsClient.GetElasticClient();


            #region index 
            //var person = new Person()
            //{
            //    Id = 1,
            //    FirstName = "zhangsan",
            //    LastName = "lin"
            //};

            //var response = client.IndexDocument(person);

            //person.Id = 2;
            //person.FirstName = "lisi";
            //person.LastName = "sisi";

            //var response2 = client.IndexDocument(person);
            #endregion

            #region search

            //var response = client.Search<Person>(s =>
            //    s.From(0)
            //    .Size(1)
            //    .Query(
            //        m => m.MatchAll()

            //    ));

            //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response.Documents.ToList()));
            //Console.ReadKey();

            #endregion

            #region searchrequest 

            var searchRequest = new SearchRequest<Person>(Nest.Indices.All, Types.All)
            {
                From = 0,
                Size = 10,
                Query = (new MatchQuery
                {
                    Field = Infer.Field<Person>(f => f.FirstName),
                    Query = "Martijn",
                    
                }) && new BoolQuery()
                {
                    
                },
                    
            };
            

            #endregion

            #region aggregations 

            var response = client.Search<Person>(s =>
                s.From(0)
                    .Size(1)
                    .Query(
                        m => m.MatchAll()

                    ).Aggregations(a => a
                        .Terms("last_names", ta => ta
                            .Field(f => f.LastName)
                        )
                    )
            );
            

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(response.Documents.ToList()));


            //参考：https://stackoverflow.com/questions/37697866/nest-conditional-filter-query-with-multiple-terms


            Console.ReadKey();

            #endregion
        }
    }

    [ElasticsearchType(Name = "person", IdProperty = "id")]
    public class Person
    {
        public long Id { get; set; }

        [Text]
        
    
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
