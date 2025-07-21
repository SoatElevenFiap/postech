namespace Soat.Eleven.FastFood.Core.Interfaces.Services;

public interface IPasswordService
{
    string Generate(string password);
    bool Equal(string currentPassword, string beforePassword);
}
