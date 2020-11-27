using System;
using System.Collections.Generic;
using System.Text;


namespace WordFamilies
{
    public class CombinationGenerator
    {
        public List<string> Generate(int length)
        {
            List<string> values = new List<string>();
            StringBuilder sb;

            for (int i = 0; i < (Math.Pow(2, length)); i++) 
            {
                string s = Convert.ToString(Convert.ToInt32(i), 2);

                if(s.Length != length)
                {
                    sb = new StringBuilder(s);
                    sb.Append("0", 1, length-s.Length);
                }

                values.Add(sb.ToString());
            }


            return values;
        }

    }
}
