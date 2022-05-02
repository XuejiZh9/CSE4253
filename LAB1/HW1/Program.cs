using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HW1
{
    class Program
    {
        [System.STAThread]
        static void Main(string[] args)
        {
            String[] temp = new String[1000];
            String clipStr = ""; 
            bool strFlag = Clipboard.ContainsText(); // Flag to check if there is anything in clipboard
            char[] separators = new char[] { ',', '.', ':', ';', '!', '?','{','}','%','&','+','=', ' ', '\n', '[', ']', '"', '\'', '(', ')','/','<','>','$','\r'};

            if (strFlag) //check if the clipboard is empty
            {
                clipStr = Clipboard.GetText(TextDataFormat.UnicodeText);
                clipStr = clipStr.ToLower();
                temp = clipStr.Split(separators, 1000, StringSplitOptions.RemoveEmptyEntries);  // separate the words
                temp = temp.Distinct().ToArray();
                Array.Sort(temp);   //sort the word=

                foreach (var words in temp) 
                {
                    foreach (var sep in separators)
                    {
                        if (words.Contains(sep))
                        {
                            words.Remove(sep); // Delete the seperators
                        }
                    }
                    Console.WriteLine(words.ToLower());
                }
            }

            Console.ReadKey();
        }
    }
}
