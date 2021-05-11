using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace KafkaProducerDemo
{
    class KafkaProducer:IDisposable
    {
        private ProducerConfig _config = new ProducerConfig();
        private IProducer<string, string> _producer;
        public KafkaProducer(string server = null)
        {
            if (string.IsNullOrEmpty(server))
            {
                server = "localhost:9092";
            }
            _config.BootstrapServers = server;
            _producer = new ProducerBuilder<string, string>(_config).Build();
        }

        public async Task<DeliveryResult<string, string>> ProduceAsync(string topic, Message<string, string> message)
        {
            return await _producer.ProduceAsync(topic, message);
            
        }

        public void Dispose()
        {
            _producer?.Dispose();
        }
    }
}
