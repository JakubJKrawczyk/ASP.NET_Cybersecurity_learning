using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace FrontApp.Hasher;

public class Hasher
{
    private  static Byte[] _salt;
    
    private const int keySize = 64;
    private const int iterations = 350000;
    private static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
    public Hasher()
    {
        _salt = RandomNumberGenerator.GetBytes(0);
    }

    public static string HashPassword(string password)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        SHA256 sha = SHA256.Create();
        var hash = sha.ComputeHash(bytes);

        return Convert.ToHexString(hash);
    }
    
    public static bool VerifyPassword(string password, string hash)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        SHA256 sha = SHA256.Create();
        var hashToCompare = sha.ComputeHash(bytes);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
}