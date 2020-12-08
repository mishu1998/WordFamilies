using System.Collections.Generic;

namespace WordFamilies
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
}
