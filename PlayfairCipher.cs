using System.Net.Security;
using System.Security;

namespace PlayfairCipher
{
    /// <summary>
    /// 
    /// </summary>
    class Cipher
    {
        private string? key;
        private string? message;
        private string? encryptedMessage;
        private string? decryptedMessage;

        private char[,] keySquare = new char[5, 5];
        
        List<char> alphabets = new List<char>();
        List<string> diagraphs = new List<string>();

        public Cipher() {}

        public Cipher(string key, string message)
        {
            this.key = key.ToUpper();
            this.message = message.Replace(" ", string.Empty);
            GenerateAlphabets();

            Console.WriteLine("Plain Text: " + message);
            Console.WriteLine("\nKey: " + key);
        }

        public void GenerateAlphabets()
        {
            string alphabetString = "ABCDEFGHIKLMNOPQRSTUVWXYZ";

            for (int i = 0; i < alphabetString.Length; i++)
            {
                alphabets.Add(alphabetString[i]);
            }

            for (int i = 0; i < key.Length; i++)
            {
                alphabets.Remove(key[i]);
            }
        }

        public bool ExistsInKeySquare(char c)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (keySquare[i, j] == c)
                        return true;
                }
            }
            return false;
        }

        public int[] IndexInKeySquare(char c)
        {
            int[] indexes = new int[2];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (keySquare[i, j] == c)
                    {
                        indexes[0] = i;
                        indexes[1] = j;
                        return indexes;
                    }
                }
            }
            return indexes;
        }

        public void GenerateKeySquare()
        {
            int keyLength = key.Length;
            int keyIndex = 0;
            int alphabetIndex = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (keyIndex < keyLength)
                    {
                        bool InKeySquare = ExistsInKeySquare(key[keyIndex]);

                        if (InKeySquare == false)
                        {
                            keySquare[i, j] = key[keyIndex++];
                        }
                        else
                        {
                            keyIndex++;
                            keySquare[i, j] = key[keyIndex++];
                        }
                    }
                    else
                    {
                        keySquare[i, j] = alphabets[alphabetIndex++];
                    }
                }
            }
            Console.WriteLine("\n ------ Key Square ------ \n ");

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(" | " +keySquare[i, j]);
                }
                Console.WriteLine(" |");
            }
        }

        public void CreateDiagraphs()
        {
            int plainTextLength = message.Length;
            int plainIndex = 0;

            for (int i = 0; i < plainTextLength; i += 2)
            {
                string diag = "";
                
                // increment by 2 at each step and make diagraphs

                if (plainIndex + 1 < plainTextLength)
                {
                    char firstChar = message[plainIndex];
                    char secondChar = message[plainIndex + 1];

                    // if both letters of diagrah same, replace second with 'x'

                    if (firstChar == secondChar)
                    {
                        diag = string.Concat(firstChar, 'X');
                        diagraphs.Add(diag);
                        plainIndex++;
                    }
                    else
                    {
                        diag = string.Concat(firstChar, secondChar);
                        diagraphs.Add(diag);
                        plainIndex +=2;
                    }
                }
                //diagraphs.Add(diag);
            }
            // condition if odd and last keyChar is jumped over in +2 loop

            if (plainIndex < plainTextLength)
            {
                string diag = "";
                diag = string.Concat(message[plainIndex], 'Z');
                diagraphs.Add(diag);
            }

            Console.WriteLine(" \n ------ Diagrahs ------ \n ");

            for (int i = 0; i < diagraphs.Count; i++)
            {
                Console.WriteLine(i + ": " + diagraphs[i]);
            }
        }
        
        public void Encrypt()
        {
            for (int i = 0; i < diagraphs.Count; i++)
            {
                char firstChar = diagraphs[i][0];
                char secondChar = diagraphs[i][1];

                int[] firstCharIndex = IndexInKeySquare(firstChar);
                int[] secondCharIndex = IndexInKeySquare(secondChar);

                int i_firstChar = firstCharIndex[0];
                int j_firstChar = firstCharIndex[1];

                int i_secondChar = secondCharIndex[0];
                int j_secondChar = secondCharIndex[1];

                // if both char in same column

                if (j_firstChar == j_secondChar)
                {
                    char firstEncryptedChar = keySquare[(i_firstChar + 1) % 5, j_firstChar];
                    char secondEncryptedChar = keySquare[(i_secondChar + 1) % 5, j_secondChar];
                    encryptedMessage += string.Concat(firstEncryptedChar, secondEncryptedChar);
                }
                else if (i_secondChar == i_firstChar)
                {
                    char firstEncryptedChar = keySquare[i_firstChar, (j_firstChar + 1) % 5];
                    char secondEncryptedChar = keySquare[i_secondChar, (j_secondChar + 1) % 5];
                    encryptedMessage += string.Concat(firstEncryptedChar, secondEncryptedChar);
                }
                else
                {
                    char firstEncryptedChar = keySquare[i_firstChar, j_secondChar];
                    char secondEncryptedChar = keySquare[i_secondChar, j_firstChar];
                    encryptedMessage += string.Concat(firstEncryptedChar, secondEncryptedChar);
                }
            }
            Console.Write("\nEncrypted Message: ");

            for (int i = 0; i < encryptedMessage.Length; i++)
            {
                Console.Write(encryptedMessage[i]);
            }
            Console.WriteLine();
        }

        public void Decrypt()
        {
            for (int i = 0; i < encryptedMessage.Length; i+=2)
            {
                char firstChar = encryptedMessage[i];
                char secondChar = encryptedMessage[i+1];

                int[] firstCharIndex = IndexInKeySquare(firstChar);
                int[] secondCharIndex = IndexInKeySquare(secondChar);

                int i_firstChar = firstCharIndex[0];
                int j_firstChar = firstCharIndex[1];

                int i_secondChar = secondCharIndex[0];
                int j_secondChar = secondCharIndex[1];

                // if both char in same column

                if (j_firstChar == j_secondChar)
                {
                    char firstDecryptedChar = keySquare[((i_firstChar - 1) % 5) < 0 ? ((i_firstChar - 1) % 5) + 5 : ((i_firstChar - 1) % 5), j_firstChar];
                    char secondDecryptedChar = keySquare[((i_secondChar - 1) % 5) < 0 ? ((i_secondChar - 1) % 5) + 5 : ((i_secondChar - 1) % 5), j_secondChar];
                    decryptedMessage += string.Concat(firstDecryptedChar, secondDecryptedChar);
                }
                else if (i_secondChar == i_firstChar)
                {
                    char firstDecryptedChar = keySquare[i_firstChar, ((j_firstChar - 1) % 5) < 0 ? ((j_firstChar - 1) % 5) + 5 : ((j_firstChar - 1) % 5)];
                    char secondDecryptedChar = keySquare[i_secondChar, ((j_secondChar - 1) % 5) < 0 ? ((j_secondChar - 1) % 5) + 5 : ((j_secondChar - 1) % 5)];
                    decryptedMessage += string.Concat(firstDecryptedChar, secondDecryptedChar);
                }
                else
                {
                    char firstDecryptedChar = keySquare[i_firstChar, j_secondChar];
                    char secondDecryptedChar = keySquare[i_secondChar, j_firstChar];
                    decryptedMessage += string.Concat(firstDecryptedChar, secondDecryptedChar);
                }
            }
            Console.Write("\nDecrypted Message: ");

            for (int i = 0; i < decryptedMessage.Length; i++)
            {
                Console.Write(decryptedMessage[i]);
            }
            Console.WriteLine();
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Cipher playFair = new Cipher("MONARCHY", "INSTRUMENTS");
            playFair.GenerateKeySquare();
            playFair.CreateDiagraphs();
            playFair.Encrypt();
            playFair.Decrypt();
        }
    }

}
