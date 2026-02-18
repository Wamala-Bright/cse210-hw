using System;

public abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;
    protected bool _isComplete;

    public string Name => _name;
    public bool IsComplete => _isComplete;

    protected Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract int RecordEvent();
    public abstract string GetSaveString();

    public virtual string GetDetailsString()
    {
        return $"{_name} ({_description}) - Completed: {_isComplete}";
    }
}
