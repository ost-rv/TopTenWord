using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TopTenWord
{
    class Program
    {
        static void Main(string[] args)
        {
            string text;
            if (TryGetText(out text))
            {
                var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());

                string[] words = noPunctuationText.Split(new string[]{"\n", " " }, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, int> dictionaryWordCount = new Dictionary<string, int>();
                string keyWord;
                
                foreach (string word in  words)
                {
                    keyWord = word.ToLower();
                    if (dictionaryWordCount.ContainsKey(keyWord))
                    {
                        dictionaryWordCount[keyWord] += 1;
                    }
                    else
                    {
                        dictionaryWordCount.Add(keyWord, 1);
                    }
                }

                var topWords  = dictionaryWordCount.OrderByDescending(t => t.Value).Take(10);

                int number = 0;
                foreach(var item in topWords)
                {
                    number += 1;
                    Console.WriteLine($"Место {number}: {item.Key.ToUpper()} встречается {item.Value} раз.");
                }
            }

            Console.ReadKey();
        }

        private static bool TryGetText(out string text)
        {
            text = string.Empty;
            try
            {
                string fileName = "Text1.txt";
                if (File.Exists(fileName))
                {
                    text = File.ReadAllText("Text1.txt");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Файл {fileName} не найден");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
