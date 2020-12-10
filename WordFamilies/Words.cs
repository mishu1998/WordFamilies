using System;
using System.Collections.Generic;
using System.Text;

namespace WordFamilies
{
    /// <summary>
    /// This class is used to store statistics about a specific word that 
    /// are used to calcualte the score for the algorithm
    /// </summary>
    public class Words
    {
        // The word stored as string
        public string word;

        // Lenght of the word
        public int length;

        // An array for each lettern in the alfabet
        public int[] letterFreq = new int[26];

        // Number of vowels present in the word
        public int vowels;


        /// <summary>
        /// Determines said statistics
        /// </summary>
        /// <param name="s"> The word itself </param>
        public Words(string s)
        {
            word = s;
            length = s.Length;

            for (int i = 0; i < s.Length; i++)
            {
                letterFreq[s[i] - 97]++;

                if (s[i] == 97  ||
                    s[i] == 101 ||
                    s[i] == 105 ||
                    s[i] == 111 ||
                    s[i] == 117 )
                    vowels++;
            }
        }
    }
}
