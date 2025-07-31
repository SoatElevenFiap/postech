namespace Soat.Eleven.FastFood.Application.Services;

public interface IPasswordService
{
    string Generate(string password);
    bool Equal(string currentPassword, string beforePassword);
}
