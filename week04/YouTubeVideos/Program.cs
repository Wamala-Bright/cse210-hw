using System;
using System.Collections.Generic;

// This program demonstrates abstraction by using Video and Comment classes
// to store and display information about YouTube videos and their comments.

class Program
{
    static void Main(string[] args)
    {
        // Create videos
        Video video1 = new Video("Learning C# Basics", "Code Academy", 600);
        Video video2 = new Video("Top 10 Football Goals", "Sports Hub", 480);
        Video video3 = new Video("How to Cook Pasta", "Chef Daily", 720);

        // Add comments to video 1
        video1.AddComment(new Comment("Alice", "Very helpful tutorial!"));
        video1.AddComment(new Comment("Brian", "Clear and easy to understand."));
        video1.AddComment(new Comment("Cynthia", "Loved the examples."));

        // Add comments to video 2
        video2.AddComment(new Comment("David", "Amazing goals!"));
        video2.AddComment(new Comment("Emma", "That last goal was insane."));
        video2.AddComment(new Comment("Frank", "Best compilation so far."));

        // Add comments to video 3
        video3.AddComment(new Comment("Grace", "I tried this recipe, it works!"));
        video3.AddComment(new Comment("Henry", "Simple and tasty."));
        video3.AddComment(new Comment("Irene", "Can you do more recipes?"));

        // Put videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display video information and comments
        foreach (Video video in videos)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");

            foreach (Comment comment in video.Comments)
            {
                Console.WriteLine($"- {comment.Name}: {comment.Text}");
            }
        }
    }
}

class Video
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int Length { get; private set; }
    public List<Comment> Comments { get; private set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }
}

class Comment
{
    public string Name { get; private set; }
    public string Text { get; private set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}
