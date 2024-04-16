using System;
using System.Net.Security;

namespace VigenereCipher
{
    /// <summary>
    /// Vigenere Cipher is a polyalphabetic cipher, meaning it has more than one substitution for a single letter
    /// </summary>
    class Cipher
    {
        private string? shiftKey;
        private string? message;
        private string? encryptedMessage;
        private string? decryptedMessage;

        private readonly char[] alphabets;

        public string Message
        {
            get { return message != null ? message : ""; }
            set { message = value; }
        }

        public string ShiftKey
        {
            get { return shiftKey != null ? shiftKey : "Key in null"; }
            set { shiftKey = value; }
        }

        public char[] Alphabets
        {
            get { return alphabets; }
        }

        public Cipher() { alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(); }
        public Cipher(string message)
        {
            //this.shiftKey = shiftKey;
            this.message = message.Replace(" ", string.Empty);
            alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        }

        //public bool checkLetter(char letter)
        //{
        //    return alphabets.Contains(letter);
        //}

        public string Encrypt()
        {
            if (message != null)
            {
                for (int i = 0; i < message.Length; i++)
                {
                    char plainChar = char.ToUpper(message[i]);
                    int indexPlainChar = Array.IndexOf(alphabets, plainChar);
                    int keyIndex = Array.IndexOf(alphabets, shiftKey[i]);
                    int encryptedCharIndex = (indexPlainChar + keyIndex ) % (26);
                    char encryptedChar = alphabets[encryptedCharIndex];
                    encryptedMessage = encryptedMessage + encryptedChar;
                }
                Console.WriteLine("Encrypted Message: " + encryptedMessage);
                return encryptedMessage != null ? encryptedMessage : "Error in encrypting";
            }
            Console.WriteLine("Original Message Empty");
            return "";
        }

        public string Decrypt()
        {
            if (encryptedMessage != null)
            {
                for (int i = 0; i < encryptedMessage.Length; i++)
                {
                    char encryptedChar = encryptedMessage[i];
                    int index = Array.IndexOf(alphabets, encryptedChar);
                    int keyIndex = Array.IndexOf(alphabets, shiftKey[i]);
                    int decryptedCharIndex = (index - keyIndex + 26) % (26);
                    char decryptedChar = alphabets[decryptedCharIndex];
                    decryptedMessage = decryptedMessage + decryptedChar;
                }
                Console.WriteLine("Decrypted Message: " + decryptedMessage);
                return decryptedMessage != null ? decryptedMessage : "Error in decrypting";
            }
            Console.WriteLine("Encrypted Message is empty");
            return "";
        }

        public void KeyGenerator()
        {
            Console.Write("Enter Key: ");
            string? key = Console.ReadLine().ToUpper();
            
            if (message != null && key != null)
            {
                if (key.Length == message.Length)
                {
                    this.shiftKey = key;
                    return;
                }
                else if (key.Length > message.Length)
                {
                    Console.WriteLine("Key cannot be smaller than plain text");
                    return;
                }
                else if (key.Length < message.Length)
                {
                    char[] keyArray = new char[message.Length];
                    //keyArray = key.ToCharArray();
                    key.ToCharArray().CopyTo(keyArray, 0);
                    int size = message.Length - key.Length;

                    for (int i=0; i < size; i++)
                    {
                        keyArray[i+key.Length] = key[i % key.Length];
                    }
                    string extendKey = new string(keyArray);
                    this.shiftKey = extendKey;
                    Console.WriteLine("Key Generated: " + shiftKey);
                    return;
                }
                else { Console.WriteLine("Error"); return; }
            }
            else
            {
                Console.WriteLine("Message or Key cannot be null");
                return;
            }
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------ Vigenere Cipher ----------");
            Console.Write("Enter PLain Text: ");
            string? originalText = Console.ReadLine();
            Cipher Vigenere = new Cipher(originalText);
            Vigenere.KeyGenerator();
            Vigenere.Encrypt();
            Vigenere.Decrypt();
        }
    }

}