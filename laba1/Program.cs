using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace laba1
{ 


    public class ReadTableSubstitution
    {
        public static string[,] readtable(string filename)
        {
            //считывание таблицы перестановок 
            string tablekey;
            using (var sw = new StreamReader(filename))
            {
                tablekey = sw.ReadToEnd();

            }
            string[,] tablesubstitution = new string[26, 26];

            int i = 0;
            int j = 0;
            foreach (char c in tablekey)
            {
                if (c != ' ' && c != '\n' && c != '\r')
                {
                    tablesubstitution[i, j] += c;
                }
                if (c == ' ')
                {
                    j++;
                    if (j == 26)
                    {
                        i++;
                        j = 0;
                        if (i == 26) break;
                    }
                }
            }
            return tablesubstitution;
        }
    }

    public class ReadTextInFile
    {
        public static string readtext(string filename)
        {
            string text;
            using (var sw = new StreamReader(filename))
            {
                text = sw.ReadToEnd();

            }
            return text;
        }
    }

    public class WriteTextInFile
    {
        public static void writetext(string filename,string text)
        {
            using (var sw = new StreamWriter(filename, true, Encoding.UTF8))
            {
                sw.Write(text);
            }
        }
    }


            

    class BigramCipher
    {
        private string[,] table;
        

        public BigramCipher(string[,] table)
        {
            this.table = table;
        }

        private bool Isletter(char c)
        { 
            if(c==' '||c=='\n'||c=='\r')
            {
                return false;
            }
            return true;
        }

        private int NumberLetter(char c)
        {
            switch (c)
            {
                case 'a':return 0;
                case 'b': return 1;
                case 'c': return 2;
                case 'd': return 3;
                case 'e': return 4;
                case 'f': return 5;
                case 'g': return 6;
                case 'h': return 7;
                case 'i': return 8;
                case 'j': return 9;
                case 'k': return 10;
                case 'l': return 11;
                case 'm': return 12;
                case 'n': return 13;
                case 'o':return  14;
                case 'p': return 15;
                case 'q': return 16;
                case 'r': return 17;
                case 's': return 18;
                case 't': return 19;
                case 'u': return 20;
                case 'v': return 21;
                case 'w': return 22;
                case 'x': return 23;
                case 'y': return 24;
                case 'z': return 25;
                default: return -1;
            }
        }
                   
        public void encryption(string filename)
        {
            string inputtext = ReadTextInFile.readtext(filename);
            string cryptogram = new string("");
            for (int i =0; i<inputtext.Length;i++)
            {
                if(this.Isletter(inputtext[i]))//буква
                {
                    if (this.Isletter(inputtext[i + 1])) //и вторая тоже буква то обращаемя к таблице подстановок 
                    {
                        int horizontalIndex = this.NumberLetter(inputtext[i]);
                        int verticalIndex = this.NumberLetter(inputtext[i + 1]);

                        cryptogram += this.table[horizontalIndex, verticalIndex];

                        i++;
                    }
                    else // вторая не буква
                    {
                        cryptogram += inputtext[i];
                        i++;
                        cryptogram += inputtext[i];
                    }
                }
                else //не буква
                {
                    cryptogram += inputtext[i];
                }
            }

            WriteTextInFile.writetext("cryptogram.txt", cryptogram);
        }

        private List<int> coordinatesbigram(string bigram)
        {
            List<int> coordinates = new List<int>();
            int x = table.Length;
            for (int i = 0; i <26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    if (bigram == table[i,j])
                    {
                        coordinates.Add(i);
                        coordinates.Add(j);
                        return coordinates;
                    }
                }
            }
            return coordinates;
        }
    
        private char LetterFromNumber(int index)
        {
            switch (index)
            {
                case 0: return 'a';
                case 1: return 'b';
                case 2: return 'c';
                case 3: return 'd';
                case 4: return 'e';
                case 5: return 'f';
                case 6: return 'g';
                case 7: return 'h';
                case 8: return 'i';
                case 9: return 'j';
                case 10: return 'k';
                case 11: return 'l';
                case 12: return 'm';
                case 13: return 'n';
                case 14: return 'o';
                case 15: return 'p';
                case 16: return 'q';
                case 17: return 'r';
                case 18: return 's';
                case 19: return 't';
                case 20: return 'u';
                case 21: return 'v';
                case 22: return 'w';
                case 23: return 'x';
                case 24: return 'y';
                case 25: return 'z';
                default: return ' ';
            }
        }
            
        public void decryption(string filename)
        {
            string ciphertext = ReadTextInFile.readtext(filename);
            string text = new string("");
            for (int i = 0; i < ciphertext.Length; i++)
            {
                if (this.Isletter(ciphertext[i]))//буква
                {
                    if (this.Isletter(ciphertext[i + 1])) //и вторая тоже буква то обращаемя к таблице подстановок 
                    {
                        string bigram = new string("");
                        bigram += ciphertext[i];
                        bigram += ciphertext[i + 1];

                        List<int> coord = coordinatesbigram(bigram);

                        text += LetterFromNumber(coord[0]);
                        text += LetterFromNumber(coord[1]);

                        i++;
                    }
                    else // вторая не буква
                    {
                        text += ciphertext[i];

                        i++;
                        text += ciphertext[i];
                    }
                }
                else //не буква
                {
                    text += ciphertext[i];
                }
            }

            WriteTextInFile.writetext("OriginalText.txt", text);
        }

    }








   


    class Program
    {
        static void Main(string[] args)
        {

            string[,] tablesubstitution = ReadTableSubstitution.readtable("KeyTable.txt");
            
            BigramCipher cipher = new BigramCipher(tablesubstitution);

            cipher.encryption("InputText.txt");
            
            cipher.decryption("cryptogram.txt");

            Console.WriteLine();

        }
    }
}





