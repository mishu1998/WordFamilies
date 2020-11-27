using System;
using System.Collections.Generic;
using System.Text;

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
                score = sizeScore*0.6 + lettersScore*0.4;
            }
        }

        public static List<string> combinations = new List<string>();

        public static List<WordFamilies> wordFamilies = new List<WordFamilies>();

        public static void Start(char choice)
        {
            CombinationGenerator combinationGenerator = new CombinationGenerator();
            combinations = combinationGenerator.Generate(Program.length);
            BuildWordFamilies(choice);
            string result =  EvaluateBestFamily();

            for (int i = 0; i < result.Length; i++)
            {
                if(result[i] != '*' && Program.displayWord[(2 * i + 1)] == '_')
                Program.displayWord[(2 * i + 1)] = result[i];
            }

        }

        private static void BuildWordFamilies(char letter)
        {
            wordFamilies.Clear();

            foreach (var pattern in combinations)
            {
                StringBuilder newPattern = new StringBuilder(pattern);

                for (int i = 0; i < pattern.Length; i++)
                {
                    if (pattern[i] == '1')
                        newPattern[i] = letter;
                    else
                    {
                        newPattern[i] = '*';
                    }
                }

                WordFamilies newWordFamilies = new WordFamilies { pattern = newPattern.ToString() };
                wordFamilies.Add(newWordFamilies);
            }


            for (int i = 0; i < Program.wordList.Count; i ++)
            {
                foreach (WordFamilies families in wordFamilies)
                {
                    StringBuilder patternMatch = new StringBuilder(Program.wordList[i].word);

                    for (int k = 0; k < Program.wordList[i].word.Length; k++)
                    {
                        if (families.pattern[k] == '*' && patternMatch[k] != letter)
                            patternMatch[k] = '*';

                    }

                    if (families.pattern == patternMatch.ToString())
                        families.wordList.Add(Program.wordList[i]);

                }
            }
        }
        
       static string EvaluateBestFamily()
       {
           WordFamilies BestFamily = new WordFamilies();
           for (int i = 0; i < wordFamilies.Count; i++)
           {
               if (wordFamilies[i].wordList.Count > BestFamily.wordList.Count)
                   BestFamily = wordFamilies[i];
           }
           Program.wordList.Clear();
           Program.wordList = BestFamily.wordList;

           return BestFamily.pattern;
       }
       
    }
}
