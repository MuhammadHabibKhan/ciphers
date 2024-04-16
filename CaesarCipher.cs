using System.Net.Security;

namespace CaesarCipher
{
    class Cipher
    {
        private int shiftKey;
        private string? message;
        private string? encryptedMessage;
        private string? decryptedMessage;

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

        public Cipher() { }
        public Cipher(int shiftKey, string message) 
        {
            this.shiftKey = shiftKey;
            this.message = message;
        }

        public string Encrypt()
        {
            if (message != null)
            {
                for (int i = 0; i < message.Length; i++)
                {
                    char encryptedChar = message[i];
                    int asciiValue = (int)encryptedChar;
                
                    if (asciiValue > 127)
                    {
                        return "Non-ASCII Value entered";
                    }
                    else
                    {
                        asciiValue += this.ShiftKey;
                        encryptedChar = (char)asciiValue;
                    }
                    //string.Concat(encryptedChar, encryptedMessage);
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
                    char decryptedChar = encryptedMessage[i];
                    int asciiValue = (int)decryptedChar;
                    asciiValue -= this.ShiftKey;
                    decryptedChar = (char)asciiValue;
                    //string.Concat(decryptedMessage, decryptedChar);
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
            // HELLO changes to KHOOR
            Cipher Caesar = new Cipher(3, "HELLO");
            Caesar.Encrypt();
            Caesar.Decrypt();
        }
    }

}
