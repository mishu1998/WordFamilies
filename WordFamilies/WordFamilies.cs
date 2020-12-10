using System.Collections.Generic;

namespace WordFamilies
{
/// <summary>
/// Class used to store the 'families' of words based on
/// a shared pattern determined by the guess taken by the user.
/// It also determines the score for said family which is used
/// to determine the best pattern to go forward with.
/// </summary>
    public class WordFamilies
    {
        public string pattern;
        public List<Words> wordList = new List<Words>();
        public double score = 0;

        /// <summary>
        /// The score of the 'family' gets determined by a weighted equation which is as follows:
        /// - a letter score, that weights 40% that gets further broken down into:
        ///     - 70 % number of unique consonants left in the word ( excluding already guessed letters)
        ///     - 30 % number of vowels ( based on the same idea as consonants)
        ///     
        /// - a size score, that weights 60% of the overall score 
        /// 
        /// The final score gets stored within the class variable
        /// </summary>
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
