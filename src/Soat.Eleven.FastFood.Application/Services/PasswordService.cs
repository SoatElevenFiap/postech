using System.Security.Cryptography;
using System.Text;

namespace Soat.Eleven.FastFood.Application.Services;

public static class PasswordService
{
    private static string Salt => "LhC2w472LWXN0/RMkp65Yw==";
    public static string Generate(string password)
    {
        var saltByte = Encoding.UTF8.GetBytes(Salt);
        var hmacMD5 = new HMACMD5(saltByte);
        var passwordConvert = Encoding.UTF8.GetBytes(password!);
        return Convert.ToBase64String(hmacMD5.ComputeHash(passwordConvert));
    }

    public static bool IsEqual(string currentPassword, string beforePassword)
    {
        var password = Generate(currentPassword);

        return password == beforePassword;
    }
}
