using Soat.Eleven.FastFood.Core.ValueObjects;

namespace Soat.Eleven.FastFood.Core.Entities;

public class Cliente : Usuario
{
    public DocumentCPF Cpf { get; set; }
    public DateTime DataDeNascimento { get; set; }
}
