using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void f()
        {
            StartGame();
        }

        static void StartGame()
        {
            Random rand = new Random();
            int randomNumber = rand.Next(1, 100);
            int noOfAttempts = 0;

            Console.WriteLine("Welcome to the Number Guessing Game!");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Guess a number between 1 and 100: ");
                string input = Console.ReadLine().Trim();

                if (int.TryParse(input, out int userGuess))
                {
                    noOfAttempts++;
                    if (userGuess < randomNumber)
                    {
                        Console.WriteLine("Too Low! Try again.");
                    }
                    else if (userGuess > randomNumber)
                    {
                        Console.WriteLine("Too High! Try again.");
                    }
                    else
                    {
                        Console.WriteLine($"Congratulations! You guessed it in {noOfAttempts} attempts");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 100.");
                }
            }
        }
    }
}

