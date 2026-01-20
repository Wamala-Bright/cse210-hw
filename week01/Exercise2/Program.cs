using System;

class Program
{
    static void Main(string[] args)
    {
        
        // Ask the user for their grade percentage
        Console.Write("Enter your grade percentage: ");
        string input = Console.ReadLine();
        int gradePercentage;

        // Validate input
        if (!int.TryParse(input, out gradePercentage))
        {
            Console.WriteLine("Invalid input. Please enter a number.");
            return;
        }

        // Core Requirement 1 & 2: Determine letter grade
        string letter = "";

        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Core Requirement 3: Print the letter grade
        Console.WriteLine($"Your letter grade is: {letter}");

        // Determine if the student passed
        if (gradePercentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Don't give up! Keep trying and you'll improve.");
        }

        // Stretch Challenge: Add + or - to grades
        string sign = "";

        if (letter != "A" && letter != "F") // Only add + or - for B, C, D
        {
            int lastDigit = gradePercentage % 10; // Get the last digit
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }
        else if (letter == "A" && gradePercentage < 100 && gradePercentage >= 90)
        {
            int lastDigit = gradePercentage % 10;
            if (lastDigit < 3)
            {
                sign = "-"; // Handle A- case
            }
        }

        // Print the full grade with sign
        Console.WriteLine($"Your full grade is: {letter}{sign}");
   
    }
}