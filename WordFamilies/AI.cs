using System;
using System.Collections.Generic;

namespace WordFamilies
{
    public static class AI
    {

        public static void Start(char choice)
        {
            WordFamilies result = BuildWordFamilies(choice, Program.wordList);

            Program.wordList = result.wordList;

            for (int i = 0; i < result.pattern.Length; i++)
            {
                if (result.pattern[i] != '*' && Program.displayWord[(2 * i + 1)] == '_')
                    Program.displayWord[(2 * i + 1)] = result.pattern[i];
            }

        }

        private static WordFamilies BuildWordFamilies(char letter, List<Words> wordList)
        {
            SortedList<string, WordFamilies> wordFamilies = new SortedList<string, WordFamilies>();

            foreach (Words word in wordList)
            {
                string newPattern = "";

                for (int i = 0; i < word.length; i++)
                {
                    if (word.word[i] == letter)
                        newPattern += letter;
                    else
                    {
                        newPattern += '*';
                    }
                }

                if (wordFamilies.ContainsKey(newPattern))
                {
                    wordFamilies[newPattern].wordList.Add(word);
                }
                else
                {
                    wordFamilies.Add(newPattern, new WordFamilies { wordList = new List<Words> { word }, pattern = newPattern });
                }

            }

            WordFamilies BestFamily = new WordFamilies();
            foreach (string key in wordFamilies.Keys)
            {
                wordFamilies[key].DetermineScore();
                if (wordFamilies[key].score > BestFamily.score)
                    BestFamily = wordFamilies[key];
                if (wordFamilies[key].wordList.Count != 0)
                    Console.WriteLine("Pattern = {0} with a score of {1} and word count of {2}", wordFamilies[key].pattern, wordFamilies[key].score.ToString("0.##"), wordFamilies[key].wordList.Count);
            }
            Console.WriteLine("\n \n Winner!");
            Console.WriteLine("Pattern = {0} with a score of {1} and word count of {2}", BestFamily.pattern, BestFamily.score.ToString("0.##"), BestFamily.wordList.Count);

            Console.ReadLine();

            return BestFamily;
        }
    }
}
