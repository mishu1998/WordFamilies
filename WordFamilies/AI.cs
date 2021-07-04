using System;
using System.Collections.Generic;

namespace WordFamilies
{
    public static class AI
    {
        /// <summary>
        /// Method used as a starting point of the algorithm.
        /// Initial wanted use was as an ground to itterate and recall BuildWordFamilies
        /// </summary>
        /// <param name="choice">Guess validated from the user</param>
        public static void Start(char choice)
        {
            // Algorithm gets runned and return a WordFamily
            WordFamilies result = BuildWordFamilies(choice, Program.wordList);

            // It replaces the main list
            Program.wordList = result.wordList;

            // Based on the pattern that returned from the algorithm, the display gets changed accordingly
            for (int i = 0; i < result.pattern.Length; i++)
            {
                if (result.pattern[i] != '*' && Program.displayWord[(2 * i + 1)] == '_')
                    Program.displayWord[(2 * i + 1)] = result.pattern[i];
            }

        }

        /// <summary>
        /// Built as a modular method, was meant to be able to recall the method to itterate trough the posibilities
        /// This is the method where the program analyses the patterns it has based on the letter the user has chosen and selectes
        /// the pattern with the highest score determined by a weighted equation
        /// </summary>
        /// <param name="letter"> Guess taken by the user </param>
        /// <param name="wordList"> Word list to look for patterns </param>
        /// <returns> Best pattern based on the score </returns>
        private static WordFamilies BuildWordFamilies(char letter, List<Words> wordList)
        {
            // We generate a sorted list to hold all the possible patterns determined, with the key being the pattern itself
            SortedList<string, WordFamilies> wordFamilies = new SortedList<string, WordFamilies>();

            // Iterate trough each word to find the pattern of it
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

                // If the pattern matches an existing pattern, the word gets added to the according 'family' of words that share the same pattern
                if (wordFamilies.ContainsKey(newPattern))
                {
                    wordFamilies[newPattern].wordList.Add(word);
                }

                // If the pattern is new, a new 'family' gets generated with said pattern and adds the word in question
                else
                {
                    wordFamilies.Add(newPattern, new WordFamilies { wordList = new List<Words> { word }, pattern = newPattern });
                }

            }

            // Variable used to hold the 'family' with the best score
            WordFamilies BestFamily = new WordFamilies();

            // Iterate trough each pattern that has been found
            foreach (string key in wordFamilies.Keys)
            {
                // Determines the score of said 'family'
                wordFamilies[key].DetermineScore();

                // Replaces the best 'family' with the current one if the score is higher
                if (wordFamilies[key].score > BestFamily.score)
                    BestFamily = wordFamilies[key];

                //Used for debugging
                if (wordFamilies[key].wordList.Count != 0)
                   Console.WriteLine("Pattern = {0} with a score of {1} and word count of {2}", wordFamilies[key].pattern, wordFamilies[key].score.ToString("0.##"), wordFamilies[key].wordList.Count);
            }

            // Used for debugging
            Console.WriteLine("\n \n Winner!");
            Console.WriteLine("Pattern = {0} with a score of {1} and word count of {2}", BestFamily.pattern, BestFamily.score.ToString("0.##"), BestFamily.wordList.Count);
            Console.ReadLine();

            return BestFamily;
        }
    }
}
