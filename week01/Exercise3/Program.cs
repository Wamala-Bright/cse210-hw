using System;

class Program
{
    static void Main(string[] args)
    {
         bool playAgain = true; // For the stretch challenge

        while (playAgain)
        {
            // Generate a random number from 1 to 100
            Random random = new Random();
            int magicNumber = random.Next(1, 101);

            int guess = 0;
            int guessCount = 0; // Stretch: count number of guesses

            Console.WriteLine("I have picked a magic number between 1 and 100.");
            
            // Loop until the user guesses the correct number
            while (guess != magicNumber)
            {
                Console.Write("Enter your guess: ");
                string input = Console.ReadLine();

                // Validate input
                if (!int.TryParse(input, out guess))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                guessCount++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine($"You guessed it! The magic number was {magicNumber}.");
                    Console.WriteLine($"It took you {guessCount} guesses."); // Stretch: show guesses
                }
            }

            // Stretch: ask if the user wants to play again
            Console.Write("Do you want to play again? (yes/no): ");
            string response = Console.ReadLine().ToLower();

            if (response != "yes")
            {
                playAgain = false;
                Console.WriteLine("Thanks for playing! Goodbye.");
            }
        }
    
    }
}