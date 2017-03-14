using EasyNetQ.Topology;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyNetQ.Consumer.demo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //var advancedBus = RabbitHutch.CreateBus("host=localhost").Advanced;
                //var queueName = "my.queue";
                //var routingKey = "A.*";
                //var queue = advancedBus.QueueDeclare(queueName);
                //var exchange = advancedBus.ExchangeDeclare("my.exchange", ExchangeType.Topic);
                //var binding = advancedBus.Bind(exchange, queue, routingKey);
                //var i = 0;
                //while (i < 100)
                //{
                //    Console.WriteLine("i=" + i);
                //    advancedBus.Consume(queue, (body, properties, info) => Task.Factory.StartNew(() =>
                //    {
                //        var message = Encoding.UTF8.GetString(body);
                //        Console.WriteLine("Got message:" + message);

                //        //Console.WriteLine("Got message: '{0}'", message);
                //    }));
                //    i++;
                //    Thread.Sleep(5000);
                //};
                //Console.ReadKey();


                var advancedBus = RabbitHutch.CreateBus("host=192.168.203.128;requestedHeartbeat=300;timeout=0;username=admin;password=admin").Advanced;
                if (advancedBus.IsConnected)
                {
                    var queueName = "ZNJB.Order";
                    var routingKey = "ZNJB.*";
                    var queue = advancedBus.QueueDeclare(queueName);

                    var count = advancedBus.MessageCount(queue);
                    //var result = advancedBus.Get(queue);

                    var exchange = advancedBus.ExchangeDeclare("ZNJB.exchange", ExchangeType.Topic);
                    var binding = advancedBus.Bind(exchange, queue, routingKey);
                    advancedBus.Consume(queue, (body, properties, info) => Task.Factory.StartNew(() =>
                    {
                        //Thread.Sleep(3000);
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Got message:" + message);
                        //Console.WriteLine("Got message: '{0}'", message);
                    }));
                    //advancedBus.Consume(queue, (body, properties, info) => GetBody(body, properties, info));





                    //Console.WriteLine("***i={0}\n***v={1}\n***", i, nowcount);

                    //var i = 0;
                    //while (i < 100)
                    //{
                    //    advancedBus.Consume(queue, (body, properties, info) => GetBody(body, properties, info));
                    //    i++;
                    //    Thread.Sleep(5000);
                    //}


                    //var message = new Message<TraceableCodeSub>(new TraceableCodeSub());
                    //advancedBus.Consume<Message<TraceableCodeSub>>(queue, (body, info) => Task.Factory.StartNew(() =>
                    //{
                    //    var message = JsonConvert.SerializeObject(body);
                    //    Console.WriteLine("Got message:" + message);
                    //    //Console.WriteLine("Got message: '{0}'", message);
                    //}));


                }

                Console.ReadKey();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            

        }


        private static  void GetBody(byte[] body, MessageProperties properties, MessageReceivedInfo info)
        {
            try
            {
                if (body == null || body.Length <= 0)
                {
                    Console.WriteLine("没有数据*********");
                    Thread.Sleep(10000);

                }
                string message = string.Empty;
                message = Encoding.UTF8.GetString(body);
            
            //await Task.Run(() =>
            //    {
            //        return message = Encoding.UTF8.GetString(body);
            //    });

                Console.WriteLine(message);
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }


    }



}