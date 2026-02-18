using System;

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points, bool isComplete = false)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            Console.WriteLine($"Completed {_name}! +{_points} points");
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
