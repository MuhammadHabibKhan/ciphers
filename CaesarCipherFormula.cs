using System.Net.Security;

namespace CaesarCipherFormula
{
    /// <summary>
    /// Caesar Cipher is a monoalphabetic cipher meaning it substitute a single character for each unique letter in the original text.
    /// The encryption works using a numeric key also known as a 'shift' which moves up the position of the original character in the alphabet by the shift value.
    /// Decryption is simply done by subtracting that key from the encrypted text.
    /// Since a shift of 26 is the limitation for this cipher, it can be easily broken using brute force.
    /// </summary>
    
    class Cipher
    {
        private int shiftKey;
        private string? message;
        private string? encryptedMessage;
        private string? decryptedMessage;

        private readonly char[] alphabets;

        public string Message
        {
            get { return message != null ? message : ""; }
            set { message = value; }
        }

        public int ShiftKey
        {
            get { return shiftKey; }
            set { shiftKey = value; }
        }

        public char[] Alphabets
        {
            get { return  alphabets; }
        }

        public Cipher() { alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(); }
        public Cipher(int shiftKey, string message)
        {
            this.shiftKey = shiftKey;
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
                    int index = Array.IndexOf(alphabets, plainChar);
                    //int index = Array.FindIndex(alphabets, checkLetter);
                    int encryptedCharIndex = (index + shiftKey) % (26);
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
                    int decryptedCharIndex = (index - shiftKey + 26) % (26); // addition of 26 loops back the decryption incase key is larger than index which results in negative value and out of bounds exception
                    char decryptedChar = alphabets[decryptedCharIndex];
                    decryptedMessage = decryptedMessage + decryptedChar;
                }
                Console.WriteLine("Decrypted Message: " + decryptedMessage);
                return decryptedMessage != null ? decryptedMessage : "Error in decrypting";
            }
            Console.WriteLine("Encrypted Message is empty");
            return "";
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------ Caesar Cipher ----------");
            Console.Write("Enter PLain Text: ");
            string? originalText = Console.ReadLine();
            Console.Write("Enter Shift Key: ");

            try
            {
                int key = int.Parse(Console.ReadLine());

                if (key < 0 || key > 25)
                {
                    Console.WriteLine("Please enter a valid key between 0 and 25");
                    return;
                }
                else if (string.IsNullOrEmpty(originalText))
                {
                    Console.WriteLine("Please enter plain text to be encrypted");
                    return;
                }

                Cipher Caesar = new Cipher(key, originalText);
                Caesar.Encrypt();
                Caesar.Decrypt();
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter a valid key");
                //throw;
                return;
            }
        }
    }

}