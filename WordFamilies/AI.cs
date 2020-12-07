using System;
using System.Collections.Generic;

namespace WordFamilies
{
    public static class AI
    {
        public class WordFamilies
        {
            public string pattern;
            public List<Words> wordList = new List<Words>();
            public double score = 0;

            public void DetermineScore()
            {
                double lettersScore = 0;

                for (int i = 0; i < wordList.Count; i++)
                {
                    double uniqueLetters = 0;

                    for (int j = 0; j < 26; j++)
                    {
                        if (wordList[i].letterFreq[j] != 0 && Program.lettersGuessed.Contains((char)i) == false)
                            uniqueLetters++;
                    }
                    double consScore = (double)(uniqueLetters - wordList[i].vowels) / 26;
                    double vowelScore = (double)wordList[i].vowels / 26;
                    lettersScore += consScore * 0.7 + vowelScore * 0.3;
                }
                double sizeScore = (double)wordList.Count / Program.wordList.Count;
                lettersScore /= wordList.Count;
                score = sizeScore * 0.6 + lettersScore * 0.4;
            }
        }

        public static SortedList<string, WordFamilies> wordFamilies = new SortedList<string, WordFamilies>();

        public static void Start(char choice)
        {
            BuildWordFamilies(choice);

            string result = EvaluateBestFamily();

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] != '*' && Program.displayWord[(2 * i + 1)] == '_')
                    Program.displayWord[(2 * i + 1)] = result[i];
            }

        }

        private static void BuildWordFamilies(char letter)
        {
            wordFamilies.Clear();

            foreach (Words word in Program.wordList)
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
        }

        static string EvaluateBestFamily()
        {
            WordFamilies BestFamily = new WordFamilies();
            foreach (string key in wordFamilies.Keys)
            {
                wordFamilies[key].DetermineScore();
                if (wordFamilies[key].score > BestFamily.score)
                    BestFamily = wordFamilies[key];
                if (wordFamilies[key].wordList.Count != 0)
                    Console.WriteLine("Pattern = {0} with a score of {1} and word count of {2}", wordFamilies[key].pattern, wordFamilies[key].score.ToString("0.##"), wordFamilies[key].wordList.Count);
            }
            Program.wordList.Clear();
            Program.wordList = BestFamily.wordList;
            Console.WriteLine("\n \n Winner!");
            Console.WriteLine("Pattern = {0} with a score of {1} and word count of {2}", BestFamily.pattern, BestFamily.score.ToString("0.##"), BestFamily.wordList.Count);

            Console.ReadLine();
            return BestFamily.pattern;
        }


        static string EvaluateBestFamilyExtra()
        {
            WordFamilies BestFamily = new WordFamilies();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    char letter = (char)(j + 97);

                    if (Program.lettersGuessed.Contains(letter))
                    {
                        continue;
                    }


                }
            }



            return "";
        }

    }
}
