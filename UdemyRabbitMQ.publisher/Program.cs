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

            channel.ExchangeDeclare("logs-fanout",durable:true,type:ExchangeType.Fanout);

            Enumerable.Range(1, 50).ToList().ForEach(x =>
             {
                 string message = $"log{x}";

                 var messageBody = Encoding.UTF8.GetBytes(message);

                 channel.BasicPublish("logs-fanout","", null, messageBody);

                 Console.WriteLine($"Mesaj gönderilmiştir : {message}");
             });

            

            Console.ReadLine();
        }
    }
}
