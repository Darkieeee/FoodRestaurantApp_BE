using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

class HashGenerator
{
    public static HMAC CreateHMAC(HashAlgorithmName hashAlgorithm, byte[] salt)
    {
        if (hashAlgorithm.Equals(HashAlgorithmName.SHA256))
        {
            return new HMACSHA256(salt);
        }
        else if (hashAlgorithm.Equals(HashAlgorithmName.MD5))
        {
            return new HMACMD5(salt);
        }
        else
        {
            throw new ArgumentException("Algorithm is not supported yet");
        }
    }

    public static string GenerateGuid()
    {
        return Guid.NewGuid().ToString();
    }

    public static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        HMAC hmac = CreateHMAC(HashAlgorithmName.MD5, salt);
        byte[] bytes = Convert.FromHexString(password);

        return Convert.ToHexString(hmac.ComputeHash(bytes));
    }

    public static void Main()
    {
        Console.Write(HashPassword("123456"));
    }
}