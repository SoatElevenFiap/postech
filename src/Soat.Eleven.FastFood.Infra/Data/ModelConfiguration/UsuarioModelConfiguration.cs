using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Infra.Data.ModelConfiguration;

public class UsuarioModelConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(c => c.Nome)
               .IsRequired();

        builder.Property(c => c.Email)
               .IsRequired();

        builder.Property(c => c.Perfil)
               .IsRequired();

        builder.Property(c => c.CriadoEm)
               .HasColumnType("timestamp")
               .HasDefaultValueSql("NOW()");

        builder.Property(c => c.ModificadoEm)
               .HasColumnType("timestamp")
               .HasDefaultValueSql("NOW()")
               .ValueGeneratedOnAddOrUpdate();

        builder.Property(c => c.Perfil)
               .HasConversion<string>();

        builder.Property(c => c.Status)
               .HasDefaultValue(StatusUsuario.Ativo)
               .HasConversion<string>();
    }
}
