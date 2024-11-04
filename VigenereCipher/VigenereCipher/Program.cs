using System;
using System.Linq;
using System.Text;

public class VigenereCipher
{
    private const string RomanianAlphabet = "AĂÂBCDEFGHIÎJKLMNOPQRSȘTȚUVWXYZ";

    private static string FormatString(string input)
    {
        return new string(input.Where(c => RomanianAlphabet.Contains(char.ToUpper(c))).Select(char.ToUpper).ToArray());
    }

    public static string Encrypt(string message, string key)
    {
        message = FormatString(message);
        key = FormatString(key);
        char[] result = message.ToCharArray();

        for (int i = 0; i < message.Length; i++)
        {
            int keyIndex = i % key.Length;
            int messageIndex = RomanianAlphabet.IndexOf(message[i]);
            int keyIndexChar = RomanianAlphabet.IndexOf(key[keyIndex]);

            if (messageIndex >= 0 && keyIndexChar >= 0)
            {
                int encryptedCharIndex = (messageIndex + keyIndexChar) % RomanianAlphabet.Length;
                result[i] = RomanianAlphabet[encryptedCharIndex];
            }
        }

        return new string(result);
    }

    public static string Decrypt(string message, string key)
    {
        message = FormatString(message);
        key = FormatString(key);
        char[] result = message.ToCharArray();

        for (int i = 0; i < message.Length; i++)
        {
            int keyIndex = i % key.Length;
            int messageIndex = RomanianAlphabet.IndexOf(message[i]);
            int keyIndexChar = RomanianAlphabet.IndexOf(key[keyIndex]);

            if (messageIndex >= 0 && keyIndexChar >= 0)
            {
                int decryptedCharIndex = (messageIndex - keyIndexChar + RomanianAlphabet.Length) % RomanianAlphabet.Length;
                result[i] = RomanianAlphabet[decryptedCharIndex];
            }
        }

        return new string(result);
    }

    public static bool ValidateRomanianText(string text)
    {
        text = FormatString(text);
        return text.All(c => RomanianAlphabet.Contains(c));
    }

    public static void Main()
    {
        // Set console input and output encoding to UTF-8
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("Enter the message (or type 'exit' to quit):");
            string message = Console.ReadLine() ?? string.Empty;
            if (message.ToLower() == "exit") break;

            Console.WriteLine("Enter the key (must be at least 7 characters):");
            string key = Console.ReadLine() ?? string.Empty;
            if (key.Length < 7)
            {
                Console.WriteLine("The key must be at least 7 characters long.");
                continue;
            }

            Console.WriteLine("Would you like to encipher (e) or decipher (d)?");
            string choice = Console.ReadLine()?.ToLower() ?? string.Empty;

            if (!ValidateRomanianText(message) || !ValidateRomanianText(key))
            {
                Console.WriteLine("Message or key contains invalid characters. Only Romanian alphabet letters are allowed.");
                continue;
            }

            if (choice == "e")
            {
                string encrypted = Encrypt(message, key);
                Console.WriteLine("Enciphered message: " + encrypted);
            }
            else if (choice == "d")
            {
                string decrypted = Decrypt(message, key);
                Console.WriteLine("Deciphered message: " + decrypted);
            }
            else
            {
                Console.WriteLine("Invalid choice. Please type 'e' for encipher or 'd' for decipher.");
            }
        }
    }
}
