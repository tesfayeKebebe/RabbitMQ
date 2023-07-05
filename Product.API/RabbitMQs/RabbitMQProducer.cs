using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Product.API.RabbitMQs;

public class RabbitMQProducer: IRabbitMQProducer
{
    public void SendMessage<T>(T message)
    {
        //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
        var factory= new ConnectionFactory()
        {
            HostName = "localhost"
        };
//Create the RabbitMQ connection using connection factory details as i mentioned above
        var connection = factory.CreateConnection();
//Here we create channel with session and model
        using var channel = connection.CreateModel();
//declare the queue after mentioning name and a few property related to that
        channel.QueueDeclare("product", exclusive: false);
        //Serialize the message
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange:"", routingKey:"productKey", body:body);
//Set Event object which listen message from chanel which is sent by producer
        // var consumer = new EventingBasicConsumer(channel);
        // consumer.Received += (model, eventArgs) =>
        // {
        //     var body = eventArgs.Body.ToArray();
        //     var message = Encoding.UTF8.GetString(body);
        //
        // }
    }
}