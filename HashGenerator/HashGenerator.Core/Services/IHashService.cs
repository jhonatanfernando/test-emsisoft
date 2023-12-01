using HashGenerator.Core.Dtos;

namespace HashGenerator.Core.Services;

public interface IHashService
{
    Task<int> CreateAsync(CreateHashDto hash, CancellationToken token = default);

    Task<IEnumerable<HashDto>> GetAllAsync(CancellationToken token = default);

    Task<IEnumerable<GroupHashDto>> GetAllGroupedAsync(CancellationToken token = default);
}

