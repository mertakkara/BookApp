using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RAbbitMQProducer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "BookQueue", durable: false, exclusive: false, autoDelete: false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) => {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Product message received: {message}");
            };

            channel.BasicConsume(queue: "BookQueue", autoAck: true, consumer: consumer);
            Console.ReadKey();
        }
    }
}
