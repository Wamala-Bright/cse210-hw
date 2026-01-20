using System;

class Program
{
    static void Main(string[] args)
    {
         List<int> numbers = new List<int>();
        int inputNumber;

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        // Collect numbers until user enters 0
        do
        {
            Console.Write("Enter number: ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out inputNumber))
            {
                Console.WriteLine("Please enter a valid number.");
                continue;
            }

            if (inputNumber != 0)
            {
                numbers.Add(inputNumber);
            }

        } while (inputNumber != 0);

        // Core Requirement 1: Sum
        int sum = numbers.Sum();
        Console.WriteLine($"The sum is: {sum}");

        // Core Requirement 2: Average
        double average = numbers.Count > 0 ? numbers.Average() : 0;
        Console.WriteLine($"The average is: {average}");

        // Core Requirement 3: Largest number
        int largest = numbers.Count > 0 ? numbers.Max() : 0;
        Console.WriteLine($"The largest number is: {largest}");

        // Stretch Challenge: Smallest positive number
        int? smallestPositive = numbers.Where(n => n > 0).DefaultIfEmpty(int.MaxValue).Min();
        if (smallestPositive == int.MaxValue)
        {
            Console.WriteLine("No positive numbers entered.");
        }
        else
        {
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }

        // Stretch Challenge: Sorted list
        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }
    
    }
}