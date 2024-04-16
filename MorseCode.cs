using System;
using System.Collections.Generic;
using System.Threading;

namespace MorseCode
{
    class Morse
    {
        private string message;
        private string morseCode;
        private string decrytedText;

        readonly Dictionary<char, string> Code = new Dictionary<char, string>();

        public Morse(string msg)
        {
            this.message = msg.ToUpper();
            //this.message = message.Replace(" ", string.Empty);

            // populate the dictionary with Morse codes
            // American Morse Code used here with long dashes

            Code.Add('A', ".-");
            Code.Add('B', "-...");
            Code.Add('C', "-.-.");
            Code.Add('D', "-..");
            Code.Add('E', ".");
            Code.Add('F', "..-.");
            Code.Add('G', "--.");
            Code.Add('H', "....");
            Code.Add('I', "..");
            Code.Add('J', ".---");
            Code.Add('K', "-.-");
            Code.Add('L', ".-..");
            Code.Add('M', "--");
            Code.Add('N', "-.");
            Code.Add('O', "---");
            Code.Add('P', ".--.");
            Code.Add('Q', "--.-");
            Code.Add('R', ".-.");
            Code.Add('S', "...");
            Code.Add('T', "-");
            Code.Add('U', "..-");
            Code.Add('V', "...-");
            Code.Add('W', ".--");
            Code.Add('X', "-..-");
            Code.Add('Y', "-.--");
            Code.Add('Z', "--..");


            // numerals
            Code.Add('0', "-----");
            Code.Add('1', ".----");
            Code.Add('2', "..---");
            Code.Add('3', "...--");
            Code.Add('4', "....-");
            Code.Add('5', ".....");
            Code.Add('6', "-....");
            Code.Add('7', "--...");
            Code.Add('8', "---..");
            Code.Add('9', "----.");
        }

        // Following function only works if dictionary consists of unique values
        public char GetKeyFromValue(string valueVar)
        {
            foreach (char keyVar in Code.Keys)
            {
                if (Code[keyVar] == valueVar)
                {
                    return keyVar;
                }
            }
            return ' ';
        }

        public void Encrypt()
        {
            morseCode = "";

            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] == ' ')
                {
                    morseCode += "  "; // space where space is meant to be
                }
                else
                {
                    morseCode += Code[message[i]] + " "; // add space after each char to decrypt easily
                }
            }
            Console.WriteLine("Message: " + message + "\n");
            Console.WriteLine("Morse Code: " + morseCode + "\n");
        }

        public void Decrypt()
        {
            int index = 0;
            string charMorseCode = "";
            decrytedText = "";
            
            while (index < morseCode.Length)
            {
                if (morseCode[index] !=  ' ')
                {
                    charMorseCode += morseCode[index];
                }
                else if (morseCode[index] == ' ')
                {
                    char decryptedChar = GetKeyFromValue(charMorseCode); // extract key / char using value / code component
                    decrytedText += decryptedChar;
                    charMorseCode = ""; // clear the variable for next character
                }
                index++;
            }
            Console.WriteLine("Decrypted Code: " + decrytedText + "\n");
        }

        public void PlayMorseCode()
        {
            for (int i = 0; i < morseCode.Length; i++)
            {
                if (morseCode[i] == '.')
                {
                    Console.Beep(); // sound for dits
                }
                else if (morseCode[i] == '-')
                {
                    Console.Beep(800, 500); // sound for dahs
                }
                else if (morseCode[i] == ' ')
                {
                    Thread.Sleep(700); // no sound for in b/w words and characters
                }
            }
            Console.WriteLine("Playback Complete \n");
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Morse code = new Morse("Connecting People");
            code.Encrypt();
            code.Decrypt();
            code.PlayMorseCode();
        }
    }
}