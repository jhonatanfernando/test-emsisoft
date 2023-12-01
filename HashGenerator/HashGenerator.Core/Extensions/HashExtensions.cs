using HashGenerator.Core.Dtos;
using HashGenerator.Core.Entities;

namespace HashGenerator.Core.Extensions;

public static class HashExtensions
{
    public static Hash ToModel(this CreateHashDto hashDto)
    {
        return new()
        {
            Date = hashDto.Date,
            Sha1 = hashDto.Sha1
        };
    }

    public static HashDto ToDto(this Hash hash)
    {
        return new()
        {
            Date = hash.Date,
            Sha1 = hash.Sha1,
            Id = hash.Id
        };
    }

    public static IEnumerable<HashDto> ToDto(this IEnumerable<Hash> hashes)
    {
        return hashes.Select(c => c.ToDto());
    }

    public static GroupHashDto ToDto(this GroupHash groupHash)
    {
        return new()
        {
            Date = groupHash.Date,
            Count = groupHash.Count
        };
    }

    public static IEnumerable<GroupHashDto> ToDto(this IEnumerable<GroupHash> groupHashes)
    {
        return groupHashes.Select(c => c.ToDto());
    }
}

