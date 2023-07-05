using System.Text;
using System.Text.Json;
using rabbitMQ_Consumer.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace rabbitMQ_Consumer.Services;

public class ProductRabbitMQConsumer
{
    private readonly IConnection connection;
    private readonly IModel channel;
    private readonly string queueName = "productKey";
    public static event Func<List<Product>, Task> MessageReceived;
    public async void Setup(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory() { HostName = "host.docker.internal" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "user-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var productJson = Encoding.UTF8.GetString(body);
                var receivedProducts = JsonSerializer.Deserialize<List<Product>>(productJson);
                MessageReceived.Invoke(receivedProducts);
            };
            channel.BasicConsume(queue: "user-queue",
                autoAck: true,
                consumer: consumer);

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
            }
        }
    }
}