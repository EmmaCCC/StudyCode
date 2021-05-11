using System;
using System.Collections.Generic;
using System.Text;
using Nest;

namespace ESDemo
{
    public class HighLevelEsClient
    {
        private static readonly ConnectionSettings _settings =  new ConnectionSettings(new Uri("http://localhost:9200"))
            .DefaultIndex("people");

        private static readonly ElasticClient _client = new ElasticClient(_settings);
       
        public static ElasticClient GetElasticClient()
        {
            return _client;
        }
    }
}
