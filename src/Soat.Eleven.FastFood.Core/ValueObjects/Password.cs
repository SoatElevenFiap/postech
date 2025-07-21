using System.Text.RegularExpressions;

namespace Soat.Eleven.FastFood.Core.ValueObjects;

public class Password
{
    private readonly string Hash;

    public Password(string hash)
    {
        Hash = hash;
        Validate();
    }

    public static implicit operator string(Password password) => password.Hash;
    public static implicit operator Password(string email) => new(email);

    private void Validate()
    {
        if (string.IsNullOrEmpty(Hash))
        {
            throw new ArgumentException("Password cannot be null or empty");
        }
        if (Hash.Length < 8)
        {
            throw new ArgumentException("Password must be at least 8 characters long");
        }
        if (!Regex.IsMatch(Hash, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[}{@#$%&?=\-_\/*\-.\|]).{8,}$"))
        {
            throw new ArgumentException("Password must contain at least one digit, one lowercase letter, one uppercase letter, and one special character");
        }
    }
}
