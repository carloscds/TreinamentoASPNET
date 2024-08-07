using APIBanco.Domain.Entidade;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.Jwt.Core.Model;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;
using System.Reflection;

namespace APIBanco.InfraEstrutura.Models;

public partial class OABContext : IdentityDbContext<Usuario>, ISecurityKeyContext
{
    public OABContext(DbContextOptions<OABContext> options) : base(options) { }

    public DbSet<KeyMaterial> SecurityKeys { get; set; }
    public virtual DbSet<Cliente> Cliente { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

}