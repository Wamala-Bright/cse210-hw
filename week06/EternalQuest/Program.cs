// Creativity Extension:
// This program exceeds core requirements by allowing users to dynamically
// create multiple goal types, persist goal data and scores using file storage,
// and earn bonus points through checklist goals for long-term engagement.

using System;
using System.Collections.Generic;
using System.IO;

// ================= BASE GOAL =================
public abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;
    protected bool _isComplete;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
        _isComplete = false;
    }

    public string Name => _name;
    public string Description => _description;
    public int Points => _points;
    public bool IsComplete => _isComplete;

    public abstract int RecordEvent();
    public abstract string GetSaveString();

    public virtual string GetDetailsString()
    {
        return $"{_name} ({_description}) - Points: {_points} - Completed: {_isComplete}";
    }
}

// ================= SIMPLE GOAL =================
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            Console.WriteLine($"Completed '{_name}' (+{_points} points)");
            return _points;
        }

        Console.WriteLine("Goal already completed.");
        return 0;
    }

    public override string GetSaveString()
    {
        return $"Simple|{_name}|{_description}|{_points}|{_isComplete}";
    }
}

// ================= ETERNAL GOAL =================
public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        Console.WriteLine($"Progress recorded for '{_name}' (+{_points} points)");
        return _points;
    }

    public override string GetSaveString()
    {
        return $"Eternal|{_name}|{_description}|{_points}";
    }
}

// ================= CHECKLIST GOAL =================
public class ChecklistGoal : Goal
{
    private int _timesCompleted;
    private int _timesRequired;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int required, int bonus)
        : base(name, description, points)
    {
        _timesCompleted = 0;
        _timesRequired = required;
        _bonusPoints = bonus;
    }

    public override int RecordEvent()
    {
        if (_timesCompleted < _timesRequired)
        {
            _timesCompleted++;
            int earned = _points;

            if (_timesCompleted == _timesRequired)
            {
                _isComplete = true;
                earned += _bonusPoints;
                Console.WriteLine($"Checklist complete! Bonus {_bonusPoints} points awarded.");
            }

            return earned;
        }

        Console.WriteLine("Checklist already completed.");
        return 0;
    }

    public override string GetDetailsString()
    {
        return $"{_name} ({_description}) - {_timesCompleted}/{_timesRequired} completed";
    }

    public override string GetSaveString()
    {
        return $"Checklist|{_name}|{_description}|{_points}|{_timesCompleted}|{_timesRequired}|{_bonusPoints}";
    }
}

// ================= GOAL MANAGER =================
public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void CreateGoal()
    {
        Console.WriteLine("1. Simple  2. Eternal  3. Checklist");
        string choice = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, description, points));
                break;

            case "2":
                _goals.Add(new EternalGoal(name, description, points));
                break;

            case "3":
                Console.Write("Times Required: ");
                int req = int.Parse(Console.ReadLine());

                Console.Write("Bonus Points: ");
                int bonus = int.Parse(Console.ReadLine());

                _goals.Add(new ChecklistGoal(name, description, points, req, bonus));
                break;
        }
    }

    public void RecordEvent()
    {
        DisplayGoals();
        Console.Write("Select goal: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < _goals.Count)
        {
            _score += _goals[index].RecordEvent();
        }
    }

    public void DisplayGoals()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Total Score: {_score}");
    }

    public void SaveGoals()
    {
        using StreamWriter writer = new StreamWriter("goals.txt");
        writer.WriteLine(_score);

        foreach (Goal goal in _goals)
        {
            writer.WriteLine(goal.GetSaveString());
        }
    }

    public void LoadGoals()
    {
        if (!File.Exists("goals.txt")) return;

        string[] lines = File.ReadAllLines("goals.txt");
        _score = int.Parse(lines[0]);
        _goals.Clear();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');

            if (parts[0] == "Simple")
                _goals.Add(new SimpleGoal(parts[1], parts[2], int.Parse(parts[3])));

            else if (parts[0] == "Eternal")
                _goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));

            else if (parts[0] == "Checklist")
                _goals.Add(new ChecklistGoal(parts[1], parts[2],
                    int.Parse(parts[3]), int.Parse(parts[5]), int.Parse(parts[6])));
        }
    }
}

// ================= MAIN PROGRAM =================
class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n1. Create Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Display Goals");
            Console.WriteLine("4. Display Score");
            Console.WriteLine("5. Save");
            Console.WriteLine("6. Load");
            Console.WriteLine("7. Quit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": manager.CreateGoal(); break;
                case "2": manager.RecordEvent(); break;
                case "3": manager.DisplayGoals(); break;
                case "4": manager.DisplayScore(); break;
                case "5": manager.SaveGoals(); break;
                case "6": manager.LoadGoals(); break;
                case "7": running = false; break;
            }
        }
    }
}
