using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score;

    public void CreateGoal()
    {
        Console.WriteLine("1. Simple  2. Eternal  3. Checklist");
        string type = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        if (type == "1")
            _goals.Add(new SimpleGoal(name, description, points));
        else if (type == "2")
            _goals.Add(new EternalGoal(name, description, points));
        else if (type == "3")
        {
            Console.Write("Times Required: ");
            int required = int.Parse(Console.ReadLine());

            Console.Write("Bonus Points: ");
            int bonus = int.Parse(Console.ReadLine());

            _goals.Add(new ChecklistGoal(name, description, points, 0, required, bonus));
        }
    }

    public void RecordEvent()
    {
        DisplayGoals();
        Console.Write("Choose goal: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index >= 0 && index < _goals.Count)
            _score += _goals[index].RecordEvent();
    }

    public void DisplayGoals()
    {
        for (int i = 0; i < _goals.Count; i++)
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
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
            writer.WriteLine(goal.GetSaveString());
    }

    public void LoadGoals()
    {
        if (!File.Exists("goals.txt")) return;

        string[] lines = File.ReadAllLines("goals.txt");
        _score = int.Parse(lines[0]);
        _goals.Clear();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] p = lines[i].Split('|');

            if (p[0] == "Simple")
                _goals.Add(new SimpleGoal(p[1], p[2], int.Parse(p[3]), bool.Parse(p[4])));

            else if (p[0] == "Eternal")
                _goals.Add(new EternalGoal(p[1], p[2], int.Parse(p[3])));

            else if (p[0] == "Checklist")
                _goals.Add(new ChecklistGoal(
                    p[1], p[2],
                    int.Parse(p[3]),
                    int.Parse(p[4]),
                    int.Parse(p[5]),
                    int.Parse(p[6])
                ));
        }
    }
}
