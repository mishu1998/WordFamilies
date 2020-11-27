using System;
using System.Collections.Generic;
using System.Text;

namespace WordFamilies
{
    public class CombinationGenerator
    {
        public List<string> Generate(int length)
        {

            string input = "**";
            StringBuilder tape = new StringBuilder();
            tape.Append(input);

            for (int i = 0; i < length; i++)
            {
                tape.Insert(1, '0');
            }

            int cell = tape.Length - 1;
            string state = "HALT";
            char read;

            List<string> values = new List<string>();

            do
            {
                read = tape[cell];

                switch (state)
                {
                    case "START":
                        if (read == '*')
                        {
                            tape[cell] = '*';
                            cell--;
                            state = "ADD";
                        }
                        else
                        {
                            Console.WriteLine("Error!");
                            break;
                        }
                        break;

                    case "ADD":
                        switch (read)
                        {
                            default:
                                Console.WriteLine("Error");
                                break;

                            case '0':
                                tape[cell] = '1';
                                cell++;
                                state = "RETURN";
                                break;

                            case '1':
                                tape[cell] = '0';
                                cell--;
                                state = "CARRY";
                                break;

                            case '*':
                                tape[cell] = '*';
                                cell++;
                                state = "HALT";
                                break;
                        }
                        break;

                    case "CARRY":
                        switch (read)
                        {
                            default:
                                Console.WriteLine("Error");
                                break;

                            case '0':
                                tape[cell] = '1';
                                cell++;
                                state = "RETURN";
                                break;

                            case '1':
                                tape[cell] = '0';
                                cell--;
                                state = "CARRY";
                                break;

                            case '*':
                                tape[cell] = '1';
                                cell--;
                                state = "OVERFLOW";
                                break;
                        }
                        break;

                    case "OVERFLOW":
                        if (read == '*')
                        {
                            tape[cell] = '*';
                            cell++;
                            state = "RETURN";
                        }
                        else
                        {
                            Console.WriteLine("Error!");
                            break;
                        }
                        break;

                    case "RETURN":
                        switch (read)
                        {
                            default:
                                Console.WriteLine("Error");
                                break;

                            case '0':
                                tape[cell] = '0';
                                cell++;
                                state = "RETURN";
                                break;

                            case '1':
                                tape[cell] = '1';
                                cell++;
                                state = "RETURN";
                                break;

                            case '*':
                                tape[cell] = '*';
                                state = "HALT";
                                break;
                        }
                        break;

                    case "HALT":
                        cell = tape.Length - 1;
                        StringBuilder temp = new StringBuilder(tape.ToString());
                        temp = temp.Remove(0, 1);
                        temp = temp.Remove(temp.Length - 1, 1);
                        //temp.Replace('*', ' ');
                        values.Add(temp.ToString());
                        state = "START";
                        break;

                    default:
                        Console.WriteLine("Error");
                        break;
                }
            } while (cell != 0);

            /*
            for (int j = 0; j < values.Count; j++)
            {
                int zeros = 0, ones = 0;
                for (int i = 0; i < values[j].Length; i++)
                {
                    if (values[j][i] == '0')
                        zeros++;

                    if (values[j][i] == '1')
                        ones++;
                }

                if (zeros <= 1 || ones <= 1)
                    values.Remove(values[j]);
            }
            */
            return values;
        }

    }
}
