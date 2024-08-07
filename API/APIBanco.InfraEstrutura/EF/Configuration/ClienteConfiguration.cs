using APIBanco.Domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIBanco.InfraEstrutura.EF.Validations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                       .UseIdentityColumn()
                       .ValueGeneratedOnAdd();
            builder.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            builder.Property(e => e.Endereco)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasDefaultValue("");
            builder.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            builder.HasIndex(w => w.Key).IsUnique();
        }
    }
}
