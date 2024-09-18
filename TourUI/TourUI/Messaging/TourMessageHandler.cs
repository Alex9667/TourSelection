using System.Text;
using RabbitMQ.Client;
using TourUI.Model;

namespace TourUI.Messaging
{
    public class TourMessageHandler
    {
        public void SendMessage(TourMessage tourMessage)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "tour_message", type: ExchangeType.Topic, durable: true);

            var routingKey = tourMessage.Type;
            var message = tourMessage.Name + " " + tourMessage.Email + " " + tourMessage.Location + " " + tourMessage.Type; 
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "tour_message",
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine($" [x] Sent '{routingKey}':'{message}'");
        }
    }
}
