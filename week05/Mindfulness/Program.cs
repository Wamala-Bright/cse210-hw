using System;
using System.Collections.Generic;
using System.Threading;

// EXCEEDING REQUIREMENTS:
// This program adds an extra Gratitude Activity and ensures encapsulation by
// placing shared behavior (timers, messages, animations) in the base Activity class.

class Program
{
    static void Main()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Gratitude Activity");
            Console.WriteLine("5. Quit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            Activity activity = null;

            switch (choice)
            {
                case "1": activity = new BreathingActivity(); break;
                case "2": activity = new ReflectionActivity(); break;
                case "3": activity = new ListingActivity(); break;
                case "4": activity = new GratitudeActivity(); break;
                case "5": running = false; break;
            }

            if (activity != null)
            {
                activity.Start();
            }
        }
    }
}

abstract class Activity
{
    private string _name;
    private string _description;
    protected int _duration;

    protected Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void Start()
    {
        Console.Clear();
        Console.WriteLine(_name);
        Console.WriteLine(_description);
        Console.Write("Enter duration in seconds: ");
        _duration = int.Parse(Console.ReadLine());

        Console.WriteLine("Prepare to begin...");
        ShowSpinner(3);

        Run();
        End();
    }

    protected abstract void Run();

    protected void End()
    {
        Console.WriteLine("Well done! You have completed the activity.");
        ShowSpinner(3);
        Console.WriteLine($"You completed {_name} for {_duration} seconds.");
        ShowSpinner(3);
    }

    protected void ShowSpinner(int seconds)
    {
        string[] spinner = { "|", "/", "-", "\\" };
        DateTime end = DateTime.Now.AddSeconds(seconds);
        int i = 0;
        while (DateTime.Now < end)
        {
            Console.Write(spinner[i++ % spinner.Length]);
            Thread.Sleep(250);
            Console.Write("\b");
        }
    }

    protected void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b ");
        }
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity()
        : base("Breathing Activity",
              "This activity will help you relax by walking you through breathing in and out slowly.") { }

    protected override void Run()
    {
        DateTime end = DateTime.Now.AddSeconds(_duration);
        while (DateTime.Now < end)
        {
            Console.Write("Breathe in...");
            ShowCountdown(4);
            Console.WriteLine();
            Console.Write("Breathe out...");
            ShowCountdown(4);
            Console.WriteLine();
        }
    }
}

class ReflectionActivity : Activity
{
    private List<string> _prompts = new()
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> _questions = new()
    {
        "Why was this experience meaningful to you?",
        "How did you feel when it was complete?",
        "What did you learn about yourself?",
        "How can you apply this experience in the future?"
    };

    public ReflectionActivity()
        : base("Reflection Activity",
              "This activity helps you reflect on times when you showed strength and resilience.") { }

    protected override void Run()
    {
        Random rand = new();
        Console.WriteLine(_prompts[rand.Next(_prompts.Count)]);
        ShowSpinner(5);

        DateTime end = DateTime.Now.AddSeconds(_duration);
        while (DateTime.Now < end)
        {
            Console.WriteLine(_questions[rand.Next(_questions.Count)]);
            ShowSpinner(5);
        }
    }
}

class ListingActivity : Activity
{
    private List<string> _prompts = new()
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people you have helped this week?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
        : base("Listing Activity",
              "This activity helps you list positive things in your life.") { }

    protected override void Run()
    {
        Random rand = new();
        Console.WriteLine(_prompts[rand.Next(_prompts.Count)]);
        ShowCountdown(5);

        List<string> items = new();
        DateTime end = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < end)
        {
            Console.Write("> ");
            items.Add(Console.ReadLine());
        }

        Console.WriteLine($"You listed {items.Count} items!");
    }
}

class GratitudeActivity : Activity
{
    public GratitudeActivity()
        : base("Gratitude Activity",
              "This activity helps you focus on things you are grateful for.") { }

    protected override void Run()
    {
        DateTime end = DateTime.Now.AddSeconds(_duration);
        while (DateTime.Now < end)
        {
            Console.WriteLine("Name something you are grateful for:");
            Console.ReadLine();
        }
    }
}
