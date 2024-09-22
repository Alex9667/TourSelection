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

            string type = tourMessage.Type;

            if(tourMessage.Location == "Blank")
            {
                type = "Invalid";
            }

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: type, type: ExchangeType.Topic, durable: true);

            var routingKey = type;
            var message = tourMessage.Name + "," + tourMessage.Email + "," + tourMessage.Location + "," + tourMessage.Type; 
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: type,
                                 routingKey: routingKey,
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine($" [x] Sent '{routingKey}':'{message}'");
        }
    }
}
