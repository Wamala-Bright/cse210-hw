using System;

// Creativity Extension:
// This program exceeds the core requirements by allowing users to dynamically
// create multiple goal types at runtime, persist goal data and total score
// using file storage, restore checklist goal progress when loading,
// and earn bonus points through checklist goals for long-term engagement.

class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nEternal Quest Menu");
            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Display Goals");
            Console.WriteLine("4. Display Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("7. Quit");

            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    manager.CreateGoal();
                    break;
                case "2":
                    manager.RecordEvent();
                    break;
                case "3":
                    manager.DisplayGoals();
                    break;
                case "4":
                    manager.DisplayScore();
                    break;
                case "5":
                    manager.SaveGoals();
                    break;
                case "6":
                    manager.LoadGoals();
                    break;
                case "7":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
