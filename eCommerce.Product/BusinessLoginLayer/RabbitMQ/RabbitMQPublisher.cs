using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BusinessLoginLayer.RabbitMQ;

public class RabbitMQPublisher : IRabbitMQPublisher, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly IModel _channel;
    private readonly IConnection _connection;

    public RabbitMQPublisher(IConfiguration configuration)
    {
        _configuration = configuration;
        string hostName = _configuration["RabbitMQ_HostName"]!;
        string userName = _configuration["RabbitMQ_UserName"]!;
        string password = _configuration["RabbitMQ_Password"]!;
        string port = _configuration["RabbitMQ_Port"]!;

        ConnectionFactory connectionFactory = new ConnectionFactory()
        {
            HostName = hostName,
            UserName = userName,
            Password = password,
            Port = Convert.ToInt32(port)
        };
        _connection = connectionFactory.CreateConnection();// TCP network

        _channel = _connection.CreateModel();// Virtualized channel on top of connection
    }


    public void Publish<T>(string routingKey, T message)
    {
        string messageJson = JsonSerializer.Serialize(message); // convert obj to string
        byte[] messageBodyInBytes = Encoding.UTF8.GetBytes(messageJson); // convert to byte array or binary array

        string exchangeName = _configuration["RabbitMQ_Products_Exchange"]!;

        //Create exchange or declare exchange
        _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct, durable: true);

        //Publish message into that exchange
        _channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: messageBodyInBytes);
    }

    public void Dispose()
    {
        _channel.Dispose();
        _connection.Dispose();
    }
}
