using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace UdemyRabbitMQ.subscriber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();

            factory.Uri = new Uri("amqps://njfcbmyi:LRYBUUM7GRFnO_SpgdX7E1Hf0Ra_QobR@moose.rmq.cloudamqp.com/njfcbmyi");

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            //channel.QueueDeclare("hello-queue", true, false, false);

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume("hello-queue",true,consumer);

            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine("Gelen Mesaj:" + message);
            };

            Console.ReadLine();
        }
    }
}
