using System;

public class Word
{
    private string text;
    private bool isHidden;

    public Word(string text)
    {
        this.text = text;
        isHidden = false;
    }

    // Hide this word
    public void Hide()
    {
        isHidden = true;
    }

    // Check if hidden
    public bool IsHidden()
    {
        return isHidden;
    }

    // Return display text (underscores if hidden)
    public string GetDisplayText()
    {
        if (isHidden)
            return new string('_', text.Length);
        else
            return text;
    }

    public string GetText()
    {
        return text;
    }
}
