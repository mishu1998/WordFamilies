using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace WordFamilies
{
    class Program
    {
        public static string[] words = File.ReadAllLines("dictionary.txt");

        public static StringBuilder displayWord = new StringBuilder();

        public static int length;
        public static int numberOfTries;

        public static List<Words> wordList = new List<Words>();
        public static List<char> lettersGuessed = new List<char>();

        public static int gameFinished = 0;


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


            for (int i = 0; i < length; i++)
            {
                displayWord.Append(" _");
            }
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

            while (gameFinished == 0)
            {
                Display();

                string initial = displayWord.ToString();
                int letterCount = lettersGuessed.Count;

                AI.Start(ValidateInput());

                if (initial == displayWord.ToString() && lettersGuessed.Count != letterCount)
                    numberOfTries--;

                else if (numberOfTries == 0)
                    gameFinished = 1;

                else if (displayWord.ToString().Contains('_') == false)
                    gameFinished = 2;
            }

            Console.Clear();
            Display();

            switch (gameFinished)
            {
                case 1:
                    Console.WriteLine("You lost. No more tries left.");
                    Random random = new Random();
                    Console.WriteLine("The word was {0}. Good luck next time!", wordList[random.Next(wordList.Count)]);
                    return;
                case 2:
                    Console.WriteLine("You have won! Congratulations!");
                    return;
            }

            Console.ReadKey();

        }

        private static char ValidateInput()
        {
            bool validGuess = false;
            char input = ' ';

            while (validGuess == false)
            {
                Console.WriteLine("Please take your next guess: \n");

                input = Console.ReadKey().KeyChar;
                input = char.ToLower(input);

                if (input >= 97 && input <= 122 && lettersGuessed.Contains(input) == false)
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
            lettersGuessed.Add(input);
            return input;
        }

        private static void Display()
        {
            Console.Clear();

            Console.WriteLine("Number of tries left: \t {0} \t \t Current word pool: {1}", numberOfTries, wordList.Count);
            Console.WriteLine("Letters guessed: ");

            for (int i = 0; i < lettersGuessed.Count; i++)
            {
                Console.Write(lettersGuessed[i] + " , ");
            }

            Console.WriteLine("\n \n \n" + displayWord + "\n \n");
        }


    }
}
