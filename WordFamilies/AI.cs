using System;
using System.Collections.Generic;
using System.Text;

namespace WordFamilies
{
    public static class AI
    {
        public class WordFamilies
        {
            public string root;
            public List<Words> wordList = new List<Words>();
            public int score = 0;
        }

        public static List<WordFamilies> wordFamilies = new List<WordFamilies>();

        public static void Start()
        {
            BuildWordFamilies();
            Console.ReadKey();

        }

        private static void BuildWordFamilies()
        {
            foreach (var word in Program.wordList)
            {
                if (word.has_prefix == false && word.has_suffix == false)
                {
                    WordFamilies newFamily = new WordFamilies { root = word.word, wordList = new List<Words> { word }, score = 0 };
                    wordFamilies.Add(newFamily);
                }
            }

            foreach (var word in Program.wordList)
            {
                StringBuilder temp = new StringBuilder(word.word);
                if (word.has_prefix && word.has_suffix)
                {
                    temp = temp.Remove(0, word.prefix.Length);
                    temp = temp.Remove(temp.Length - word.suffix.Length, word.suffix.Length);
                }
                else if (word.has_prefix)
                    temp = temp.Remove(0, word.prefix.Length);
                else if (word.has_suffix)
                    temp = temp.Remove(temp.Length - word.suffix.Length, word.suffix.Length);

                foreach (var family in wordFamilies)
                {
                    if (temp.ToString() == family.root && family.wordList.Contains(word) != true)
                    {
                        family.wordList.Add(word);
                    }
                }
            }

            List<WordFamilies> newwordFamilies = new List<WordFamilies>();

            for (int i = 0; i < wordFamilies.Count; i++)
            {
                if (wordFamilies[i].wordList.Count >= 2)
                {
                    newwordFamilies.Add(wordFamilies[i]);
                }
            }

            wordFamilies.RemoveRange(0,wordFamilies.Count);
            wordFamilies = newwordFamilies;
        }
    }
}
