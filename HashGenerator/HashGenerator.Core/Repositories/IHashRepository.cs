using HashGenerator.Core.Entities;

namespace HashGenerator.Core.Repositories;

public interface IHashRepository
{
    Task<int> CreateAsync(Hash hash, CancellationToken token = default);

    Task<IEnumerable<Hash>> GetAllAsync(CancellationToken token = default);

    Task<IEnumerable<GroupHash>> GetAllGroupedAsync(CancellationToken token = default);
}

