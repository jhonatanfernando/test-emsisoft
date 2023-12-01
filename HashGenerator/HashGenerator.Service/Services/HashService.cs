using HashGenerator.Core.Dtos;
using HashGenerator.Core.Entities;
using HashGenerator.Core.Extensions;
using HashGenerator.Core.Repositories;
using HashGenerator.Core.Services;

namespace HashGenerator.Service.Services;

public class HashService: IHashService
{
    private readonly IHashRepository _hashRepository;

    public HashService(IHashRepository hashRepository)
	{
        _hashRepository = hashRepository;
    }

    public Task<int> CreateAsync(CreateHashDto hash, CancellationToken token = default)
    {
        return _hashRepository.CreateAsync(hash.ToModel(), token);
    }

    public async Task<IEnumerable<HashDto>> GetAllAsync(CancellationToken token = default)
    {
        var hashes = await _hashRepository.GetAllAsync(token);

        return hashes.ToDto().ToArray();
    }

    public async Task<IEnumerable<GroupHashDto>> GetAllGroupedAsync(CancellationToken token = default)
    {
        var groupHashes = await _hashRepository.GetAllGroupedAsync(token);

        return groupHashes.ToDto();
    }
}

