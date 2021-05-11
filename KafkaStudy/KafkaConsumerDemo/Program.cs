using System;

namespace KafkaConsumerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("消费者开始消费...（默认只关注sms主题的消息)");
            using (var consumer = new KafkaConsumer())
            {
                while (true)
                {
                    consumer.Consume(a =>
                    {
                        if (a == null)
                        {
                            Console.WriteLine("暂无消息");
                        }
                        else
                        {
                            Console.WriteLine($"Key:{a.Key},Value:{a.Value}");
                        }
                    });
                }
            }
        }
    }
}
