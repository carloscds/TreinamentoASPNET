using APIBanco.Domain.Entidade;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace APIBanco.InfraEstrutura.Models;

public partial class OABContext : DbContext
{
    public OABContext(DbContextOptions<OABContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Cliente { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}