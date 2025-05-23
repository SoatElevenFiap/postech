using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Infra.Data.ModelConfiguration;

public class ClienteModelConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasOne(c => c.Usuario)
               .WithMany(u => u.Clientes)
               .HasForeignKey(c => c.UsuarioId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(c => c.Cpf)
               .IsRequired()
               .HasMaxLength(11);

        builder.Property(c => c.DataDeNascimento)
               .IsRequired();

        builder.Property(c => c.CriadoEm)
               .HasDefaultValueSql("NOW()");

        builder.Property(c => c.ModificadoEm)
               .HasDefaultValueSql("NOW()");
    }
}
