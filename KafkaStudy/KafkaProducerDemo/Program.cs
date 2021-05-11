using System;

namespace KafkaProducerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入消息内容（默认主题为sms,即短信消息类型)");
            using (var producer = new KafkaProducer())
            {
                while (true)
                {
                    string message = Console.ReadLine();
                    var result = producer.ProduceAsync("sms",
                        new Confluent.Kafka.Message<string, string>() { Key = Guid.NewGuid().ToString(), Value = message })
                        .GetAwaiter().GetResult();
                    Console.WriteLine($"offset:{result.Offset.Value},partition:{result.Partition.Value}");
                }
            }

        }
    }
}
