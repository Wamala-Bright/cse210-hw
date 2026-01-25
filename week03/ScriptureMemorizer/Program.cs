class Program
{
    static void Main(string[] args)
    {
        Reference reference = new Reference("John", 3, 16);
        string text = "For God so loved the world that he gave his one and only Son, " +
                      "that whoever believes in him shall not perish but have eternal life.";

        Scripture scripture = new Scripture(reference, text);

        while (true)
        {
            Console.Clear();
            scripture.Display();

            if (scripture.AllWordsHidden())
            {
                Console.WriteLine("All words are hidden. Memorization complete!");
                break;
            }

            Console.WriteLine($"Press Enter to hide words, or type 'quit' to exit.");
            Console.WriteLine($"Remaining words: {scripture.RemainingWords()}");
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "quit")
                break;

            // Ask user how many words to hide
            int wordsToHide = 3; // default
            Console.Write("How many words would you like to hide this turn? (Press Enter for default 3): ");
            string countInput = Console.ReadLine();
            if (int.TryParse(countInput, out int n) && n > 0)
                wordsToHide = n;

            scripture.HideRandomWords(wordsToHide);
        }
    }
}
