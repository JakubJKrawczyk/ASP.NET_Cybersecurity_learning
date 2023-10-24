using System.ComponentModel.DataAnnotations;

namespace FrontApp;

public class PasswordRequirements
{
    public static TimeSpan PasswordExpiration { get; set; }
    public static bool IsSmallLetters { get; set; } = true;
    public static string SmallLetters { get; set; } = "qwertyuiopasdfghjklzxcvbnnm";
    public static bool IsBigLetters { get; set; } = true;
    public static string BigLetters { get; set; } = "QWERTYUIOPASDFGHJKLZXCVBNM";
    public static bool IsSpecialLetters { get; set; } = false;
    public static string SpecialLetters { get; set; } = "ąćęłńóśźż!@#$%^&*";
    public static int PasswordLength { get; set; } = 8;

    public static bool CheckPassword(string password)
    {
        // password requirements
        if (PasswordRequirements.IsSmallLetters)
        {
            if (!password.Any(c => SmallLetters.Contains(c)))
            {
                return false;
            }
        }
                
        if (PasswordRequirements.IsBigLetters)
        {
            if (!password.Any(c => BigLetters.Contains(c)))
            {
                return false;
            }
        }
                
        if (PasswordRequirements.IsSpecialLetters)
        {
            if (!password.Any(c => SpecialLetters.Contains(c)))
            {
                return false;
            }
        }

        if (password.Length < PasswordLength) return false;
        return true;
    }
}