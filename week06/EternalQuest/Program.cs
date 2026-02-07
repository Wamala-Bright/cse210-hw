using System;
using System.Collections.Generic;
using System.IO;

// Base abstract class
public abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;
    private bool _isComplete;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
        _isComplete = false;
    }

    public string Name { get { return _name; } }
    public string Description { get { return _description; } }
    public int Points { get { return _points; } }
    public bool IsComplete { get { return _isComplete; } protected set { _isComplete = value; } }

    // Forces each derived class to implement how points are recorded
    public abstract int RecordEvent();

    // Default display for all goals
    public virtual string GetDetailsString()
    {
        return $"{_name} ({_description}) - Points: {_points} - Completed: {(_isComplete ? "Yes" : "No")}";
    }
}

// SimpleGoal: Completed once
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        if (!IsComplete)
        {
            IsComplete = true;
            Console.WriteLine($"You completed the goal '{Name}' and earned {Points} points!");
            return Points;
        }
        else
        {
            Console.WriteLine($"Goal '{Name}' is already completed.");
            return 0;
        }
    }
}

// EternalGoal: Never complete, repeatable
public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        Console.WriteLine($"You recorded progress on '{Name}' and earned {Points} points!");
        return Points;
    }
}

// ChecklistGoal: Must repeat multiple times for bonus
public class ChecklistGoal : Goal
{
    private int _timesCompleted;
    private int _timesRequired;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int timesRequired, int bonusPoints)
        : base(name, description, points)
    {
        _timesCompleted = 0;
        _timesRequired = timesRequired;
        _bonusPoints = bonusPoints;
    }

    public override int RecordEvent()
    {
        if (!IsComplete)
        {
            _timesCompleted++;
            int totalPoints = Points;

            Console.WriteLine($"You recorded progress on '{Name}' ({_timesCompleted}/{_timesRequired}) and earned {Points} points.");

            if (_timesCompleted >= _timesRequired)
            {
                IsComplete = true;
                totalPoints += _bonusPoints;
                Console.WriteLine($"Congratulations! You completed '{Name}' and earned a bonus of {_bonusPoints} points!");
            }

            return totalPoints;
        }
        else
        {
            Console.WriteLine($"Goal '{Name}' is already completed.");
            return 0;
        }
    }

    public override string GetDetailsString()
    {
        return $"{Name} ({Description}) - Points: {Points} - Completed: {(_timesCompleted >= _timesRequired ? "Yes" : "No")} ({_timesCompleted}/{_timesRequired})";
    }
}

// Manages all goals and total score
public class GoalManager
{
    private List<Goal> _goals;
    private int _totalScore;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _totalScore = 0;
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void RecordEvent(int index)
    {
        if (index >= 0 && index < _goals.Count)
        {
            int earned = _goals[index].RecordEvent();
            _totalScore += earned;
            Console.WriteLine($"Total Score: {_totalScore}\n");
        }
        else
        {
            Console.WriteLine("Invalid goal selection.");
        }
    }

    public void DisplayGoals()
    {
        Console.WriteLine("Your Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"\nYour total score is: {_totalScore}\n");
    }
}

// Program entry
class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();

        // Add some sample goals
        manager.AddGoal(new SimpleGoal("Run Marathon", "Complete a full marathon", 1000));
        manager.AddGoal(new EternalGoal("Read Scriptures", "Read daily scriptures", 100));
        manager.AddGoal(new ChecklistGoal("Attend Temple", "Attend the temple 10 times", 50, 10, 500));

        bool running = true;

        while (running)
        {
            Console.WriteLine("\nEternal Quest Menu:");
            Console.WriteLine("1. Display Goals");
            Console.WriteLine("2. Record Goal Progress");
            Console.WriteLine("3. Display Total Score");
            Console.WriteLine("4. Exit");

            Console.Write("Choose an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    manager.DisplayGoals();
                    break;

                case "2":
                    manager.DisplayGoals();
                    Console.Write("Enter goal number to record: ");
                    if (int.TryParse(Console.ReadLine(), out int goalNumber))
                    {
                        manager.RecordEvent(goalNumber - 1);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                    break;

                case "3":
                    manager.DisplayScore();
                    break;

                case "4":
                    running = false;
                    Console.WriteLine("Thanks for playing Eternal Quest!");
                    break;

                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }
        }
    }
}
