using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Domain.Enums;
using Soat.Eleven.FastFood.Infra.Data.ModelConfiguration.Base;

namespace Soat.Eleven.FastFood.Infra.Data.ModelConfiguration;

public class UsuarioModelConfiguration : EntityBaseModelConfiguration<Usuario>
{
    public override void Configure(EntityTypeBuilder<Usuario> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Nome)
               .IsRequired();

        builder.Property(c => c.Email)
               .IsRequired();

        builder.Property(c => c.Perfil)
               .IsRequired();

        builder.Property(c => c.Perfil)
               .HasConversion<string>();

        builder.Property(c => c.Status)
               .HasDefaultValue(StatusUsuario.Ativo)
               .HasConversion<string>();

        builder.HasData([
            new Usuario("Sistema Fast Food", "sistema@fastfood.com", "Senha@123", "11985203641", PerfilUsuario.Administrador)
        ]);
    }
}
