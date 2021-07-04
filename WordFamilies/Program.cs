using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WordFamilies
{
    class Program
    {
        // Variable used to read the txt file
        public static string[] words = File.ReadAllLines("dictionary.txt");

        // Variable used to display the dashes and letters on the screen
        public static StringBuilder displayWord = new StringBuilder();

        // Variables set by the user at runtime
        public static int length;
        public static int numberOfTries;
        public static bool showCount; 

        // Variable used to store the remaining words, used as a main list
        public static List<Words> wordList = new List<Words>();

        // Variable that holds all guesses already taken by the user
        public static List<char> lettersGuessed = new List<char>();

        // Integer variable used as a flag to denote if the game is finished or not and who won.
        public static int gameFinished = 0;


        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Guess the Word \n\n");

            // Method used to grab the lenght variable from the user
            GetWordLength();

            // Method used to grab the number of tries from the user
            GetNumberOfTries();

            GetDisplayChoice();

            // After gathering the user input the program proceedes into the algorithm
            Play();

            Console.Read();
        }



        private static void GetWordLength()
        {
            // Flag used to determine if the user input is valid
            bool validLenght = false;
            do
            {
                Console.WriteLine("Please select the word length that you wish to play (between 2 and 29): ");
                string input = Console.ReadLine();
                int.TryParse(input, out length);

                // Iterate trough the main wordlist to find if there any words matching the user input
                if (length >= 2 && length <= 29)
                {
                    foreach (var word in words)
                    {
                        // Class created where we also determine statistics about the word, to be used later in the algorithm
                        Words w = new Words(word);

                        if (w.length == length)
                        {
                            wordList.Add(w);

                            // If any word matches, the program adds it to the main list
                            validLenght = true;
                        }
                    }


                    if (validLenght == false)
                    {
                        Console.Clear();
                        Console.WriteLine("There are no words with the lenght of {0}, please select another lenght.", length);
                    }
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Selection.");

                }


            } while (validLenght == false);

            // Initialises the display of the word based on the lenght
            for (int i = 0; i < length; i++)
            {
                displayWord.Append(" _");
            }
        }

        private static void GetNumberOfTries()
        {
            Console.Clear();

            // Flag used to determine if the input is valid
            bool validNumber = false;

            while (validNumber == false)
            {
                Console.WriteLine("Please select the amount of tries that you wish to have (between 2 - 26): ");

                string input = Console.ReadLine();
                int.TryParse(input, out numberOfTries);

                // The upper limit of 26 is put in place as there are only 26 letters in total
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

        /// <summary>
        /// Gets user choice for displaying the word count
        /// </summary>
        private static void GetDisplayChoice()
        {
            bool validInput = false;

            while (validInput == false)
            {
                Console.Clear();
                Console.WriteLine("Do you wish to see the word count in the list? (Y/N)");
                char choice = Console.ReadKey().KeyChar;

                if (choice == 'y' || choice == 'Y') 
                {
                    showCount = true;
                    validInput = true;
                }

                else if (choice == 'n' || choice == 'N')
                {
                    showCount = false;
                    validInput = true;
                }

                else
                {
                    validInput = false;
                    Console.WriteLine("Invalid input!");
                }

            }
        }
        /// <summary>
        /// Method used to loop the actions up until game has finnished
        /// </summary>
        private static void Play()
        {
            // Loop cretaed to run the program while the game hasn't finished
            while (gameFinished == 0)
            {
                Display();

                // Retains initial display
                string initial = displayWord.ToString();

                // Retains initial letters guessed 
                int letterCount = lettersGuessed.Count;


                if (numberOfTries <= 0)
                {
                    // Flag changed to 1 denoting that the AI has won
                    gameFinished = 1;
                    break;
                }
                else if (displayWord.ToString().Contains('_') == false)
                {
                    // Flag changed to 2 denoting that the player has won
                    gameFinished = 2;
                    break;
                }

                // Start the algorithm based on the input that has been validated from the player
                AI.Start(ValidateInput());

                // Determines based on the changes on the display word if a try has to be substracted from the user
                if (initial == displayWord.ToString() && lettersGuessed.Count != letterCount)
                    numberOfTries--;

            }

            Console.Clear();
            Display();

            // If game has another value than 0, it will display a message according to who won
            switch (gameFinished)
            {
                case 1:
                    Console.WriteLine("You lost. No more tries left.");
                    Random random = new Random();

                    // It will show a random word from the remaining list
                    Console.WriteLine("The word was {0}. Good luck next time!", wordList[random.Next(wordList.Count)].word);
                    return;

                case 2:
                    Console.WriteLine("You have won! Congratulations!");
                    return;
            }

            Console.ReadKey();

        }

        /// <summary>
        /// Validates the input from the user, ensuring that the key pressed it is a letter
        /// </summary>
        /// <returns>A letter to be used in the algorithm</returns>
        private static char ValidateInput()
        {
            // Flag used to determine if the input is valid
            bool validGuess = false;
            char input = ' ';

            while (validGuess == false)
            {
                Console.WriteLine("Please take your next guess: \n");

                input = Console.ReadKey().KeyChar;

                // The program is running with ASCII codes of the lowercase letters, therefore we need to make sure that the input gets translated the same way
                input = char.ToLower(input);

                // We also verify if the letter has not been already guessed and display accordingly if that is the case
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
            // If guess is valid, it gets also added to the guessed list
            lettersGuessed.Add(input);
            return input;
        }

        /// <summary>
        /// Display the interface into the console
        /// </summary>
        private static void Display()
        {
            Console.Clear();

            Console.WriteLine("Number of tries left: \t {0}", numberOfTries);

            if (showCount)
            {
                Console.WriteLine("Current word pool: \t {0}", wordList.Count);
            }

            Console.WriteLine("\nLetters guessed: ");

            for (int i = 0; i < lettersGuessed.Count; i++)
            {
                Console.Write(lettersGuessed[i] + " , ");
            }

            Console.WriteLine("\n \n \n" + displayWord + "\n \n");
        }
    }
}
