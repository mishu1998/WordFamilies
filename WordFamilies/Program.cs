using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace WordFamilies
{
    class Program
    {
        public static string[] words = File.ReadAllLines("dictionary.txt");

        public static int length;
        public static int numberOfTries;

        public static List<Words> wordList = new List<Words>();
        public static List<char> lettersGuessed = new List<char>();

        public static string[] prefix = { "un", "dis", "mis", "in", "il", "im", "irr", "re", "sub", "inter", "super", "anti", "auto", "bi", "aqua", "aero", "super", "micro", "audi", "trans", "prim", "auto", "tele", "re", "pre" };
        public static string[] suffix = { "ing", "ed", "er", "est", "ied", "ier", "ing", "ment", "ness", "ful", "ly", "ation", "ous", "ology", "graph", "port" };


        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Guess the Word \n\n");

            GetWordLength();
            GetNumberOfTries();
            Play();
            Console.Read();
        }

        private static void GetWordLength()
        {
            Console.WriteLine("Please select the word length that you wish to play (between 2 and 29): ");

            bool validLenght = false;
            do
            {
                string input = Console.ReadLine();
                Int32.TryParse(input, out length);

                if (length >= 2 && length <= 29)
                {
                    foreach (var word in words)
                    {
                        Words w = new Words(word);

                        if (w.length == length)
                        {
                            wordList.Add(w);
                            validLenght = true;
                        }
                    }
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Selection.");
                    Console.WriteLine("Please select the word length that you wish to play (between 2 and 29): ");

                }

            } while (validLenght == false);
        }

        private static void GetNumberOfTries()
        {
            Console.Clear();
            bool validNumber = false;
            while (validNumber == false)
            {
                Console.WriteLine("Please select the amount of tries that you wish to have (between 2 - 26): ");

                string input = Console.ReadLine();
                Int32.TryParse(input, out numberOfTries);
                if (numberOfTries >= 2 && numberOfTries <= 26)
                {
                    validNumber = true;
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Selection.");
                }
            }
        }

        private static void Play()
        {
            AI.Start();
            while (numberOfTries != 0)
            {
                Display();
                char guess = ValidateInput();
            }
        }

        private static char ValidateInput()
        {
            bool validGuess = false;
            char input = ' ';

            while (validGuess == false)
            {
                Console.WriteLine("Please take your next guess: \n");

                input = Console.ReadKey().KeyChar;
                input = char.ToUpper(input);

                if (input >= 65 && input <= 90 && lettersGuessed.Contains(input) == false)
                {
                    validGuess = true;
                }

                else
                {
                    Console.Clear();
                    Display();
                    Console.WriteLine("Letter invalid or already guessed.");
                }
            }

            return input;
        }

        private static void Display()
        {
            Console.Clear();

            Console.WriteLine("Number of tries left: \t {0} \t \t Current word pool: {1}", numberOfTries, wordList.Count);
            Console.WriteLine("Letters guessed: \n \n \n");

            for (int i = 0; i < lettersGuessed.Count; i++)
            {
                Console.Write(lettersGuessed[i] + " , ");
            }

            StringBuilder displayWord = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                displayWord.Append(" _");
            }

            Console.WriteLine(displayWord + "\n \n");
        }


    }
}
