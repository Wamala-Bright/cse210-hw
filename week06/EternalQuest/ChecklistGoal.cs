using System;

public class ChecklistGoal : Goal
{
    private int _timesCompleted;
    private int _timesRequired;
    private int _bonusPoints;

    public ChecklistGoal(
        string name,
        string description,
        int points,
        int timesCompleted,
        int timesRequired,
        int bonusPoints
    ) : base(name, description, points)
    {
        _timesCompleted = timesCompleted;
        _timesRequired = timesRequired;
        _bonusPoints = bonusPoints;
        _isComplete = _timesCompleted >= _timesRequired;
    }

    public override int RecordEvent()
    {
        if (_isComplete)
        {
            Console.WriteLine("Checklist already completed.");
            return 0;
        }

        _timesCompleted++;
        int earned = _points;

        if (_timesCompleted >= _timesRequired)
        {
            _isComplete = true;
            earned += _bonusPoints;
            Console.WriteLine($"Checklist complete! Bonus {_bonusPoints} points!");
        }

        return earned;
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
