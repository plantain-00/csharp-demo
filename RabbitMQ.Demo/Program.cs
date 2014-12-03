using System;
using System.Text;

using RabbitMQ.Client;

namespace RabbitMQ.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory();
            IConnection conn = factory.CreateConnection();
            var channel = conn.CreateModel();

            var exchangeName = "exchangeName";
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            var queueName = "queueName";
            channel.QueueDeclare(queueName, false, false, false, null);
            var routingKey = "";
            channel.QueueBind(queueName, exchangeName, routingKey, null);

            var messageBodyBytes = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
            channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);

            bool noAck = false;
            BasicGetResult result = channel.BasicGet(queueName, noAck);
            if (result == null)
            {
                // No message available at this time.
            }
            else
            {
                IBasicProperties props = result.BasicProperties;
                byte[] body = result.Body;
                var message = Encoding.UTF8.GetString(body);
                channel.BasicAck(result.DeliveryTag, false);
            }

            channel.Close(200, "Goodbye");
            conn.Close();
        }
    }
}