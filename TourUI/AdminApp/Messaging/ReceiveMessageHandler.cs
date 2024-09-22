using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp.Messaging
{
    internal class ReceiveMessageHandler
    {
        public void ReceiveMessage()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "Invalid", type: ExchangeType.Topic, durable: true);

            var queueName = "Invalid";

            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(queue: queueName, exchange: "Invalid", routingKey: "Invalid");

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            Console.WriteLine("Waiting for messages");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received: {message}");

            };
            channel.BasicConsume(queue: "Invalid",
                                 autoAck: true,
                                 consumer: consumer);
            Console.ReadKey();
        }
    }
}
