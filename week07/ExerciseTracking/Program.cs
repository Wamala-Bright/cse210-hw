using System;
using System.Collections.Generic;

// =================== BASE CLASS ===================
abstract class Activity
{
    private string _date;
    private double _length; // minutes

    public Activity(string date, double length)
    {
        _date = date;
        _length = length;
    }

    public string Date => _date;
    public double Length => _length;

    // Abstract methods to be implemented by derived classes
    public abstract double GetDistanceMiles();
    public abstract double GetSpeedMph();
    public abstract double GetPaceMinutesPerMile();

    public virtual string GetSummary()
    {
        double distanceMiles = GetDistanceMiles();
        double distanceKm = distanceMiles / 0.62; // convert miles to km
        double speedKph = distanceKm / Length * 60; // km per hour
        double paceMinPerKm = Length / distanceKm;

        return $"{Date} {this.GetType().Name} ({Length} min) - " +
               $"Distance: {distanceMiles:0.00} miles ({distanceKm:0.00} km), " +
               $"Speed: {GetSpeedMph():0.00} mph ({speedKph:0.00} kph), " +
               $"Pace: {GetPaceMinutesPerMile():0.00} min/mile ({paceMinPerKm:0.00} min/km)";
    }
}

// =================== DERIVED CLASSES ===================

// Running
class Running : Activity
{
    private double _distanceMiles;

    public Running(string date, double length, double distanceMiles)
        : base(date, length)
    {
        _distanceMiles = distanceMiles;
    }

    public override double GetDistanceMiles() => _distanceMiles;
    public override double GetSpeedMph() => (_distanceMiles / Length) * 60;
    public override double GetPaceMinutesPerMile() => Length / _distanceMiles;
}

// Cycling
class Cycling : Activity
{
    private double _speedMph;

    public Cycling(string date, double length, double speedMph)
        : base(date, length)
    {
        _speedMph = speedMph;
    }

    public override double GetDistanceMiles() => _speedMph * (Length / 60);
    public override double GetSpeedMph() => _speedMph;
    public override double GetPaceMinutesPerMile() => 60 / _speedMph;
}

// Swimming
class Swimming : Activity
{
    private int _laps;
    private const double LapLengthMeters = 50;

    public Swimming(string date, double length, int laps)
        : base(date, length)
    {
        _laps = laps;
    }

    public override double GetDistanceMiles() => _laps * LapLengthMeters / 1000 * 0.62;
    public override double GetSpeedMph() => (GetDistanceMiles() / Length) * 60;
    public override double GetPaceMinutesPerMile() => Length / GetDistanceMiles();
}

// =================== MAIN PROGRAM ===================
class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running("03 Nov 2022", 30, 3.0),
            new Cycling("04 Nov 2022", 45, 15.0),
            new Swimming("05 Nov 2022", 60, 40)
        };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
