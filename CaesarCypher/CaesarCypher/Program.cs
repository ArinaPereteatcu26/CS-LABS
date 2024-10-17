using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace CaesarCypher
{
    internal class Program
    {
        public static char Cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {

                return ch;
            }

            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);
        }


        public static string Encipher(string input, int key)
        {
            string output = string.Empty;

            foreach (char ch in input)
            {
                if ((ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z'))
                {
                    output += Cipher(ch, key);
                }
                else
                {
                    output += ch;
                }
            }

            return output;
        }
        public static string Decipher(string input, int key)
        {
            return Encipher(input, 26 - key);
        }


        static void Main(string[] args)
        {
            int operation;
            do
            {
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("1. Encryption");
                Console.WriteLine("2. Decryption");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice (0-2): ");
            }
            while (!int.TryParse(Console.ReadLine(), out operation) || operation < 0 || operation > 2);

            if (operation == 0)
            {
                return;
            }

            int key;
            do
            {

                Console.Write("Enter your Key (1-25): ");
                bool isValidKey = int.TryParse(Console.ReadLine(), out key);

                if (!isValidKey || key < 1 || key > 25)
                {
                    Console.WriteLine("Invalid key! Please enter a value between 1 and 25.");
                }
            } while (key < 1 || key > 25);

            Console.WriteLine("\n");

            string userString;
            if (operation == 1)
            {
                do
                {
                    Console.WriteLine("Type a string to encrypt (letters A-Z, a-z):");
                    userString = Console.ReadLine();

                    if (string.IsNullOrEmpty(userString) || !IsValidString(userString))
                    {
                        Console.WriteLine("Invalid input! Please enter letters A-Z or a-z only.");
                    }
                } while (string.IsNullOrEmpty(userString) || !IsValidString(userString));

                string processedString = userString.ToUpper().Replace(" ", " ");

                Console.WriteLine("Encrypted Data:");
                string cipherText = Encipher(processedString, key);
                Console.WriteLine(cipherText);
            }
            else if (operation == 2)
            {
                do
                {
                    Console.WriteLine("Type the cryptogram to decrypt (letters A-Z, a-z):");
                    userString = Console.ReadLine();

                    if (string.IsNullOrEmpty(userString) || !IsValidString(userString))
                    {
                        Console.WriteLine("Invalid input! Please enter letters A-Z or a-z only.");
                    }
                } while (string.IsNullOrEmpty(userString) || !IsValidString(userString));


                string processedString = userString.ToUpper().Replace(" ", "");


                Console.WriteLine("Decrypted Data:");
                string decryptedText = Decipher(processedString, key);
                Console.WriteLine(decryptedText);
            }

            Console.WriteLine("\n");
            Console.ReadKey();
        }


        private static bool IsValidString(string input)
        {
            foreach (char ch in input)
            {
                if (!char.IsLetter(ch) && !char.IsWhiteSpace(ch))
                {
                    return false;
                }
            }
            return true;
        }
    }
}