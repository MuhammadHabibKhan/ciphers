using System;

namespace RailFenceCipher
{
    /// <summary>
    /// Rail fence Cipher is one of the simplest transposition ciphers. In transposition ciphers, we shuffle the positions of characters
    /// instead of changing their identities like in substitution cipher. This makes transpoisiton ciphers more complex and time complexity
    /// for encryption and decryption increases
    /// </summary>
    
    class Cipher
    {
        private int key;
        private string message;
        private string encryptedText;
        private string decryptedText;

        private char[,] CipherMatrix;
        private char[,] ReCipherMatrix;

        public Cipher(string msg, int key) 
        {
            this.key = key;
            this.message = msg.ToUpper().Replace(" ", string.Empty);

            Console.WriteLine("Message: " + message + "\n");
            Console.WriteLine("Key: " + key + "\n");
        }

        public void Encrypt()
        {
            int COL = message.Length;
            int ROW = key;

            Console.WriteLine("Zig-Zag Cipher Matrix: \n");

            CipherMatrix = new char[ROW, COL];

            int index = 0;
            bool reverseFlag = false;

            for (int i = 0; i < COL; i++)
            {
                CipherMatrix[index, i] = message[i];

                if (index == key - 1) // max depth then reverse
                    reverseFlag = true;
                
                else if (index == 0)
                    reverseFlag = false;
                
                if (reverseFlag == true)
                    index--;
                
                else
                    index++;
            }
            // print the zig-zag rail fence and encrypted text

            for (int a = 0; a < ROW; a++)
            {
                for (int b = 0; b < COL; b++)
                {
                    encryptedText += CipherMatrix[a, b];
                    Console.Write(CipherMatrix[a, b] + "\t");
                }
                Console.Write("\n");
            }
            // since array of type char is initialized by null char by default 
            // replace the spaces with empty places 
            encryptedText = encryptedText.Replace("\0", string.Empty);

            Console.WriteLine("\nEncrypted Text: " + encryptedText + "\n");
        }

        // Reconstruct Zig-Zag Matrix using Cipher Text
        public void RemakeMatrix()
        {
            int COL = encryptedText.Length;
            int ROW = key;

            ReCipherMatrix = new char[ROW, COL];

            int index = 0;
            bool reverseFlag = false;
            int encryptedTextIndex = 0;

            for (int j = 0; j < ROW; j++)
            {
                for (int i = 0; i < COL; i++)
                {
                    if (index == j)
                    {
                        ReCipherMatrix[index, i] = encryptedText[encryptedTextIndex++];
                    }

                    if (index == key - 1) // max depth then reverse
                        reverseFlag = true;

                    else if (index == 0)
                        reverseFlag = false;

                    if (reverseFlag == true)
                        index--;

                    else
                        index++;
                }
                index = 0;
            }
            // print the reconstructed matrix

            Console.WriteLine("Reconstructed Zig-Zag Cipher Matrix: \n");

            for (int a = 0; a < ROW; a++)
            {
                for (int b = 0; b < COL; b++)
                {
                    Console.Write(ReCipherMatrix[a, b] + "\t");
                }
                Console.Write("\n");
            }
        }

        public void Decrypt()
        {
            int COL = message.Length;

            int index = 0;
            bool reverseFlag = false;

            for (int i = 0; i < COL; i++)
            {
                decryptedText += ReCipherMatrix[index, i];

                if (index == key - 1) // max depth then reverse
                    reverseFlag = true;

                else if (index == 0)
                    reverseFlag = false;

                if (reverseFlag == true)
                    index--;

                else
                    index++;
            }
            Console.WriteLine("\nDecrypted Text: " + decryptedText + "\n");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Cipher Railfence = new Cipher("habib khan", 3);
            Railfence.Encrypt();
            Railfence.RemakeMatrix(); // reconstruct zig-zag matrix from cipher text
            Railfence.Decrypt();
        }
    }
}