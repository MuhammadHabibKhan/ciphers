using System.Runtime.CompilerServices;

namespace VernamCipher
{
    /// <summary>
    /// Vernam Cipher is a substitution cipher that XOR's the plain text with a key stream equal to the length of plain text.
    /// One-Time Pad is a more secure implementation of Vernam Cipher in which the key is never repeated and truly random.
    /// The following code implements the more secure One-Time Pad Cipher.
    /// Technically, every One-Time Pad Cipher is a Vernam Cipher but not every Vernam Cipher is an OTP Cipher.
    /// </summary>

    class Cipher
    {
        public string? keyStream;
        private string? message;
        private string? encryptedMessage;
        private string? decryptedMessage;
        private static readonly Random rnd = new Random(); // same instance for the whole program so it does not produce near identical values

        private readonly char[] alphabets;
        private int[] encryptedMessageIndex = new int[26];

        /// <summary>
        /// Introducing a new array here to store the original cipher text indices. We need them to calculate the accurate XOR between plain text and
        /// cipher / encrypted text. The overflow XOR value over 26 is needed else we end up with wrong decrypted text when performing XOR again.
        /// XOR is its own inverse. 
        /// </summary>

        public Cipher() { alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(); }

        public Cipher(string message)
        {
            this.message = message.Replace(" ", string.Empty);
            alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        }

        public void generateKey()
        {
            if (message != null)
            {
                for (int i = 0; i < message.Length; i++)
                {
                    char keyChar = alphabets[rnd.Next(0, 26)];
                    keyStream += char.ToUpper(keyChar);
                }
                Console.WriteLine("Key Generated: " + keyStream);
                return;
            } 
            else
            {
                Console.WriteLine("Cannot leave message empty, aborting ...");
                Environment.Exit(0);
                return;
            }
        }

        public void Encrypt()
        {
            if (message != null && keyStream != null)
            {
                Console.WriteLine("");
                Console.WriteLine("------------ Encryption ------------");

                for (int i = 0; i < message.Length; i++)
                {
                    char keyChar = keyStream[i];
                    char plainChar = char.ToUpper(message[i]);
                    int keyCharIndex = Array.IndexOf(alphabets, keyChar);
                    int plainTextIndex = Array.IndexOf(alphabets, plainChar);

                    Console.WriteLine("------------------------------------");
                    Console.WriteLine("KeyChar: " + keyChar + " Index: " + keyCharIndex);
                    Console.WriteLine("PlainChar: " + plainChar + " Index: " + plainTextIndex);

                    int encryptedCharIndex = (plainTextIndex ^ keyCharIndex); // XOR operation
                    //encryptedCharIndex += (encryptedCharIndex > 25 ? -26 : 0); // use this ternary short syntax
                    char encryptedChar = alphabets[encryptedCharIndex % 26]; // or simply mod 26 here without changing the original variable for later
                    encryptedMessageIndex[i] = encryptedCharIndex;
                    encryptedMessage += encryptedChar;

                    Console.WriteLine("Encrypted Char: " + encryptedChar + " Index: " + encryptedCharIndex);
                }
                Console.WriteLine("\n------------------------------------");
                Console.WriteLine("Encrypted Message: " + encryptedMessage);
                Console.WriteLine("------------------------------------");
                return;
            }
            Console.WriteLine("Encrypted Message or Key Stream is Empty, aborting ...");
            Environment.Exit(0);
            return;
        }

        public void Decrypt()
        {
            if (encryptedMessage != null && keyStream != null)
            {
                Console.WriteLine("");
                Console.WriteLine("------------ Decryption ------------");

                for (int i = 0; i < encryptedMessage.Length; i++)
                {
                    char keyChar = keyStream[i];
                    int keyCharIndex = Array.IndexOf(alphabets, keyChar);
                    int encryptedCharIndex = encryptedMessageIndex[i]; // taking index from the array storing original indices
                    char encryptedChar = alphabets[(encryptedCharIndex % 26)]; // mod at the end to get the correct encrypted looped char

                    Console.WriteLine("------------------------------------");
                    Console.WriteLine("KeyChar: " + keyChar + " Index: " + keyCharIndex);
                    Console.WriteLine("EncryptedChar: " + encryptedChar + " Index: " + encryptedCharIndex);

                    int decryptedCharIndex = (encryptedCharIndex ^ keyCharIndex);
                    //decryptedCharIndex += (decryptedCharIndex < 0 ? +26 : 0); // again use this or simply mod
                    char decryptedChar = alphabets[decryptedCharIndex % 26];
                    decryptedMessage += decryptedChar;

                    Console.WriteLine("Decrypted Char: " + decryptedChar + " Index: " + decryptedCharIndex);
                }
                Console.WriteLine("\n------------------------------------");
                Console.WriteLine("Decrypted Message: " + decryptedMessage);
                Console.WriteLine("------------------------------------");
                return;
            }
            Console.WriteLine("Decrypted Message or Key Stream is Empty, aborting ...");
            Environment.Exit(0);
            return;
        }

    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------- Vernam Cipher ----------");
            Console.Write("\nEnter Plain Text: ");
            string? plainText = Console.ReadLine();

            while (plainText == null)
            {
                Console.WriteLine("Plain Text cannot be empty, retrying ...");
                Console.Write("Enter Plain Text: ");
                plainText = Console.ReadLine();
            }
            Cipher Vernam = new Cipher(plainText);
            Vernam.generateKey();
            //Vernam.keyStream = "SON";
            Vernam.Encrypt();
            Vernam.Decrypt();
        }
    }
}
