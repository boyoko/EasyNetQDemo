using EasyNetQ.Topology;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EasyNetQ.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var advancedBus = RabbitHutch.CreateBus("host=localhost").Advanced;
                var queueName = "my.queue";
                var routingKey = "A.*";
                var queue = advancedBus.QueueDeclare(queueName);
                var exchange = advancedBus.ExchangeDeclare("my.exchange", ExchangeType.Topic);
                var binding = advancedBus.Bind(exchange, queue, routingKey);
                var myMessage = new MyMessage { Text = "Hello from the publisher" };
                var message = new Message<MyMessage>(myMessage);
                message.Properties.AppId = "my_app_id";
                message.Properties.ReplyTo = "my_reply_queue";

                var properties = new MessageProperties();
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                //var body = Encoding.UTF8.GetBytes("12345678");
                //advancedBus.Publish(exchange, routingKey, false, properties, body);


                Console.WriteLine("body {0}", Encoding.UTF8.GetString(body));
                for(var i = 0; i < 10000; i++)
                {
                    Console.WriteLine("i=" + i);

                    advancedBus.Publish(exchange, routingKey, false, properties, body);

                }

                Console.ReadKey();
            }
            catch(Exception e)
            {
                throw e;
            }
            

            //Console.WriteLine("Hello World!");
        }
    }


}