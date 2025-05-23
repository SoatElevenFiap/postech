using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Infra.Data.ModelConfiguration;

public class UsuarioModelConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasOne(u => u.Perfil)
               .WithMany(p => p.Usuarios)
               .HasForeignKey(c => c.PerfilId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasDefaultValueSql("gen_random_uuid()");

        builder.Property(c => c.Nome)
               .IsRequired();

        builder.Property(c => c.Email)
               .IsRequired();

        builder.Property(c => c.CriadoEm)
               .HasDefaultValueSql("NOW()");

        builder.Property(c => c.ModificadoEm)
               .HasDefaultValueSql("NOW()");
    }
}
