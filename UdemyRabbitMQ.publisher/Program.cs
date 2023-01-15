using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UdemyRabbitMQ.publisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();

            factory.Uri = new Uri("amqps://njfcbmyi:LRYBUUM7GRFnO_SpgdX7E1Hf0Ra_QobR@moose.rmq.cloudamqp.com/njfcbmyi");

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.QueueDeclare("hello-queue",true,false,false);

            Enumerable.Range(1, 50).ToList().ForEach(x =>
             {
                 string message = $"message{x}";

                 var messageBody = Encoding.UTF8.GetBytes(message);

                 channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);

                 Console.WriteLine($"Mesaj gönderilmiştir : {message}");
             });

            

            Console.ReadLine();
        }
    }
}
