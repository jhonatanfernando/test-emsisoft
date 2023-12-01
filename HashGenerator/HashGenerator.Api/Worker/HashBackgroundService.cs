using System.Text;
using HashGenerator.Api.Infrastructure;
using HashGenerator.Core.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HashGenerator.Api.Worker;

public class HashBackgroundService : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<HashBackgroundService> _logger;
    private IModel channel;

    public HashBackgroundService(IConfiguration configuration, IServiceScopeFactory scopeFactory, ILogger<HashBackgroundService> logger)
    {
        _configuration = configuration;
        _scopeFactory = scopeFactory;
        _logger = logger;

        var connection = RabbitMQExtensions.GetRabbitMQConnection(_configuration);
        channel = connection.CreateModel();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!stoppingToken.IsCancellationRequested)
        {
            channel.QueueDeclare(queue: Core.Constants.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    using (IServiceScope scope = _scopeFactory.CreateScope())
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        IHashService _hashService = scope.ServiceProvider.GetRequiredService<IHashService>();

                        await _hashService.CreateAsync(new Core.Dtos.CreateHashDto()
                        {
                            Date = DateTimeOffset.Now.Date,
                            Sha1 = message
                        });

                        _logger.LogInformation($"Processed: {message}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Exception: {ex.Message}");
                }
            };

            channel.BasicConsume(queue: Core.Constants.QueueName, autoAck: true, consumer: consumer);

        }

        return Task.CompletedTask;
    }
}

