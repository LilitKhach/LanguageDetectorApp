using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LanguageDetectorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding unicode = Encoding.Unicode;
            System.Console.InputEncoding = unicode;

            Console.WriteLine("Please, write a text to detect a language:");
            var text = Console.ReadLine();
            Console.WriteLine();
            if (text.Any(char.IsDigit))
            {
                Console.WriteLine("Enter only letters, please");
                text = Console.ReadLine();
            }

            text = Regex.Replace(text, @"\s+", "");

            var language = CheckLanguage(text);
            Console.WriteLine(language);
        }

        static string CheckLanguage(string text)
        {
            var letters = text.ToUpper();
            Dictionary<string, int> Numbers = new Dictionary<string, int>();
            string language = "No language detected";

            var countAM = 0;
            var countEN = 0;
            var countRU = 0;

            foreach (var letter in letters)
            {
                if (Alphabet.ArmenianAlphabet.ToUpper().Contains(letter))
                {
                    countAM++;
                }

                else if (Alphabet.EnglishAlphabet.ToUpper().Contains(letter))
                {
                    countEN++;
                }

                else if (Alphabet.RussianAlphabet.ToUpper().Contains(letter))
                {
                    countRU++;
                }

            }

            Numbers.Add("Armenian", countAM);
            Numbers.Add("English", countEN);
            Numbers.Add("Russian", countRU);

            var max = Numbers.Values.ToList().Max();
            if (max == letters.Length)
                language = Numbers.Where(n => n.Value == max).First().Key;

            return language;
        }
    }
}
