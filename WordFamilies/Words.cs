using System;
using System.Collections.Generic;
using System.Text;

namespace WordFamilies
{
    public class Words
    {
        public string word;
        public int length;
        public int[] letterFreq = new int[26];
        public int vowels;


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
