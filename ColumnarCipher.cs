using System;
using System.Collections.Generic;
using System.Linq;

namespace ColumnarCipher
{
    class Cipher
    {
        private string key;
        private string plainText;
        private string encryptedMessage;
        private string decryptedMessage;

        private double ROW;
        private int COL;

        char[] alphabet;
        char[,] encryptionMatrix;
        char[,] decryptionMatrix;

        List<int> Order = new List<int>();

        public Cipher(string k, string message)
        {
            this.key = k.ToUpper();
            //this.plainText = message.Replace(" ", string.Empty);
            //this.plainText = plainText.ToUpper();
            this.plainText = message.ToUpper();

            Console.WriteLine("\nPlain Text: " + plainText + "\n");
            Console.WriteLine("Key: " + key + "\n");

            this.COL = key.Length;
            this.ROW = Math.Ceiling( plainText.Length / (double) COL);

            encryptionMatrix = new char[(int) ROW, COL];
            decryptionMatrix = new char[(int)ROW, COL];

            alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            FindOrder();
        }

        public void createEncryptionMatrix()
        {
            int plainTextIndex = 0;

            Console.WriteLine("Encryption Matrix: \n");

            for (int k = 0; k < key.Length; k++)
            {
                Console.Write(" |" + key[k] + "| ");
            }
            Console.WriteLine("\n-----------------------");

            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    try
                    {
                        encryptionMatrix[i, j] = plainText[plainTextIndex++];
                    }
                    catch (Exception e)
                    {
                        encryptionMatrix[i, j] = ' ';
                    }
                    Console.Write(" |" + encryptionMatrix[i, j] + "| ");
                }
                Console.Write("\n");
            }
        }

        public void FindOrder()
        {
            // find order to extract values from encryption matrix

            for (int k = 0; k < key.Length; k++)
            {
                char keyChar = key[k];
                int order = Array.IndexOf(alphabet, keyChar);
                Order.Add(order);
            }
        }

        public void extractCipherText()
        {
            List<int> OrderCopy = new List<int>(Order);

            for (int j = 0; j < COL; j++)
            {
                int minValue = OrderCopy.Min();
                int minIndex = Order.FindIndex(x => x == minValue);
            
                for (int i = 0; i < ROW; i++)
                {
                    encryptedMessage += encryptionMatrix[i, minIndex];
                }
                OrderCopy.Remove(minValue);
            }
            Console.WriteLine("\nEncrypted Message: " + encryptedMessage);
        }

        public void createDecryptionMatrix()
        {
            Console.WriteLine("\nDecryption Matrix: \n");

            List<int> OrderCopy = new List<int>(Order);

            int encryptedMessageIndex = 0;

            for (int j = 0; j < COL; j++)
            {
                int minValue = OrderCopy.Min();
                int minIndex = Order.FindIndex(x => x == minValue);

                for (int i = 0; i < ROW; i++)
                {
                    decryptionMatrix[i, minIndex] = encryptedMessage[encryptedMessageIndex++];
                }
                OrderCopy.Remove(minValue);
            }

            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    Console.Write(" |" + decryptionMatrix[i, j] + "| ");
                }
                Console.Write("\n");
            }
        }

        public void extractPlainText()
        {
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    if (decryptionMatrix[i, j] != '$')
                        decryptedMessage += decryptionMatrix[i, j];
                }
            }
            Console.WriteLine("\nDecrypted Message: " + decryptedMessage + "\n");
        }

    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Cipher Columnar = new Cipher("khan", "habib khan");
            Columnar.createEncryptionMatrix();
            Columnar.extractCipherText();
            Columnar.createDecryptionMatrix();
            Columnar.extractPlainText();
        }
    }
}
