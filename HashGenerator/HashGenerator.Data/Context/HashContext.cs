using Microsoft.EntityFrameworkCore;
using HashGenerator.Core.Entities;

namespace HashGenerator.Data.Context;

public class HashContext : DbContext
{
    public virtual DbSet<Hash> Hashes { get; set; }

    public HashContext(DbContextOptions<HashContext> options) : base(options)
    {
    
	}
}