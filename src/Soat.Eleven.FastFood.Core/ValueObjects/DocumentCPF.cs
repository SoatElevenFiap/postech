namespace Soat.Eleven.FastFood.Core.ValueObjects;

public class DocumentCPF
{
    private readonly string _cpf;
    public DocumentCPF(string cpf)
    {
        _cpf = cpf;
        Validate();
    }
    public static implicit operator string(DocumentCPF document) => document._cpf;
    public static implicit operator DocumentCPF(string cpf) => new(cpf);
    private void Validate()
    {
        if (string.IsNullOrEmpty(_cpf))
        {
            throw new ArgumentException("CPF cannot be null or empty");
        }
        if (_cpf.Length != 11)
        {
            throw new ArgumentException("CPF must be 11 digits long");
        }
    }
}
