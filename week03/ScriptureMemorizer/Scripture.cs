public class Scripture
{
    private Reference reference;
    private List<Word> words;
    private WordSelector selector;

    public Scripture(Reference reference, string text)
    {
        this.reference = reference;
        words = new List<Word>();
        selector = new WordSelector();

        string[] splitWords = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (string w in splitWords)
        {
            words.Add(new Word(w));
        }
    }

    // Hide a specific number of words using WordSelector
    public void HideRandomWords(int count = 3)
    {
        List<int> indicesToHide = selector.SelectWordsToHide(words, count);
        foreach (int index in indicesToHide)
        {
            words[index].Hide();
        }
    }

    public bool AllWordsHidden()
    {
        foreach (var word in words)
        {
            if (!word.IsHidden())
                return false;
        }
        return true;
    }

    public void Display()
    {
        Console.WriteLine(reference.GetDisplayText());
        foreach (var word in words)
        {
            Console.Write(word.GetDisplayText() + " ");
        }
        Console.WriteLine("\n");
    }

    public int RemainingWords()
    {
        int count = 0;
        foreach (var word in words)
        {
            if (!word.IsHidden()) count++;
        }
        return count;
    }
}
