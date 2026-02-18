using System;

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        Console.WriteLine($"Progress recorded for {_name}! +{_points} points");
        return _points;
    }

    public override string GetSaveString()
    {
        return $"Eternal|{_name}|{_description}|{_points}";
    }
}
