using RabbitMQ.Client;

namespace HashGenerator.Api.Infrastructure;

public static class RabbitMQExtensions
{
    public static IConnection GetRabbitMQConnection(IConfiguration configuration)
    {
        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQ:HostName"],
            UserName = configuration["RabbitMQ:UserName"],
            Password = configuration["RabbitMQ:Password"]
        };

        return factory.CreateConnection();
    }
}

