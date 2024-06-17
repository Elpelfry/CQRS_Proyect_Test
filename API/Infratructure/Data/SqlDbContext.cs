using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Infratructure.Data;

public class SqlDbContext : DbContext
{
    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options){}

    public DbSet<Prioridades> Prioridades { get; set; }
}

