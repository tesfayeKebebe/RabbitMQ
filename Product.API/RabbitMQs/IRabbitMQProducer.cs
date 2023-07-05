namespace Product.API.RabbitMQs;

public interface IRabbitMQProducer
{
   void SendMessage<T> (T message);
}