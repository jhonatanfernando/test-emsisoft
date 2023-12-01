using HashGenerator.Core.Entities;
using HashGenerator.Core.Repositories;
using HashGenerator.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HashGenerator.Data.Repositories;

public class HashRepository: IHashRepository
{
    private readonly HashContext _hashContext;

    public HashRepository(HashContext hashContext)
	{
        _hashContext = hashContext;
    }

    public Task<int> CreateAsync(Hash hash, CancellationToken token = default)
    {
        _hashContext.Hashes.Add(hash);

        return _hashContext.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<Hash>> GetAllAsync(CancellationToken token = default)
    {
        return await _hashContext.Hashes.ToListAsync(token);
    }

    public async Task<IEnumerable<GroupHash>> GetAllGroupedAsync(CancellationToken token = default)
    {
        return await _hashContext.Hashes
                    .GroupBy(c => c.Date)
                    .Select(p => new GroupHash()
                    {
                         Date = p.Key.Date,
                         Count = p.Count()
                    }).ToListAsync(token);
    }
}

