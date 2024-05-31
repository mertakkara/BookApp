using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;

namespace BookApp.RabbitMQ
{
    public class RabbitMQBook : IRabbitMQBook
    {
        public void SendBookMessage<T>(T message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "BookQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            var json = System.Text.Json.JsonSerializer.Serialize(message);
            var body = System.Text.Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "BookQueue", basicProperties: null, body: body);
        }
    }
}
