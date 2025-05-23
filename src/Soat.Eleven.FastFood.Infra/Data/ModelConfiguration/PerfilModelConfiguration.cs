using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Soat.Eleven.FastFood.Domain.Entidades;

namespace Soat.Eleven.FastFood.Infra.Data.ModelConfiguration;

public class PerfilModelConfiguration : IEntityTypeConfiguration<Perfil>
{
    public void Configure(EntityTypeBuilder<Perfil> builder)
    {
        builder.HasData(
        [
            new Perfil(1, "Cliente"),
            new Perfil(2, "Administrador")
        ]);
    }
}
