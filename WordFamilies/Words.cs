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
        public string prefix = "", suffix= "";
        public bool has_suffix = false, has_prefix = false, is_root = false;

        public Words(string s)
        {
            word = s;
            length = s.Length;

            for (int i = 0; i < s.Length; i++)
            {
                char upper = char.ToUpper(s[i]);
                letterFreq[upper - 65]++;

                if (upper == 65 ||
                    upper == 69 ||
                    upper == 73 ||
                    upper == 79 ||
                    upper == 85)
                    vowels++;
            }


            for (int i = 0; i < Program.prefix.Length; i++)
            {
                if (Program.prefix[i].Length < s.Length)
                {
                    if (s.Substring(0, Program.prefix[i].Length) == Program.prefix[i])
                    {
                        has_prefix = true;
                        prefix = Program.prefix[i];
                    }
                }
            }

            for (int i = 0; i < Program.suffix.Length; i++)
            {
                if (Program.suffix[i].Length < s.Length)
                {
                    if (s.Substring(s.Length - Program.suffix[i].Length, Program.suffix[i].Length) == Program.suffix[i])
                    {
                        has_suffix = true;
                        suffix = Program.suffix[i];
                    }
                }
            }

            if (has_prefix == false && has_suffix == false)
            {
                is_root = true;
            }

            else if(suffix.Length + prefix.Length > s.Length)
            {
                has_prefix = has_suffix = false;
                is_root = true;
            }
        }
    }
}
