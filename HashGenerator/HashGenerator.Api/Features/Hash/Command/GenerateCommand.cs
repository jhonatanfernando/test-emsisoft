using System.Security.Cryptography;
using System.Text;
using HashGenerator.Api.Infrastructure;
using MediatR;
using RabbitMQ.Client;

namespace HashGenerator.Api.Features.Hash.Command
{
    public class GenerateCommand : IRequest<Unit>
    {
		public GenerateCommand()
		{
		}
	}

    public class GenerateCommandHandler : IRequestHandler<GenerateCommand, Unit>
    {
        private readonly IConfiguration _configuration;

        public GenerateCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<Unit> Handle(GenerateCommand request, CancellationToken cancellationToken)
        {
            // Generate 40,000 random SHA1 hashes
            var hashes = Enumerable.Range(1, 40000)
                .Select(_ => GenerateRandomSHA1Hash())
                .ToList();


            // Split hashes into batches
            var batchSize = 1000; // Adjust the batch size based on your needs
            var hashBatches = PartitionIntoBatches(hashes, batchSize);

            // Send batches to RabbitMQ in parallel
            Parallel.ForEach(hashBatches, batch =>
            {
                using (var connection = RabbitMQExtensions.GetRabbitMQConnection(_configuration))
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: Core.Constants.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    foreach (var hash in batch)
                    {
                        var body = Encoding.UTF8.GetBytes(hash);
                        channel.BasicPublish(exchange: "", routingKey: Core.Constants.QueueName, basicProperties: null, body: body);
                    }
                }
            });


            return Task.FromResult(Unit.Value);
        }


        private string GenerateRandomSHA1Hash()
        {
            using (var sha1 = SHA1.Create())
            {
                var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString()));
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }

        private List<List<string>> PartitionIntoBatches(List<string> source, int batchSize)
        {
            var batches = new List<List<string>>();
            for (int i = 0; i < source.Count; i += batchSize)
            {
                var batch = source.Skip(i).Take(batchSize).ToList();
                batches.Add(batch);
            }
            return batches;
        }
    }
}

