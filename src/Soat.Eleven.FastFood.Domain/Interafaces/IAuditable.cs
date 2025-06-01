namespace Soat.Eleven.FastFood.Domain.Interfaces;

public interface IAuditable
{
    public DateTime CriadoEm { get; set; }
    public DateTime ModificadoEm { get; set; }
}
