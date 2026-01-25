using System;
using System.Collections.Generic;

public class WordSelector
{
    private Random random = new Random();

    // Select a number of unhidden words to hide
    public List<int> SelectWordsToHide(List<Word> words, int count)
    {
        List<int> indices = new List<int>();

        // Collect indices of words that are not yet hidden
        List<int> unhiddenIndices = new List<int>();
        for (int i = 0; i < words.Count; i++)
        {
            if (!words[i].IsHidden())
                unhiddenIndices.Add(i);
        }

        int wordsToHide = Math.Min(count, unhiddenIndices.Count);

        for (int i = 0; i < wordsToHide; i++)
        {
            int randomIndex = random.Next(unhiddenIndices.Count);
            indices.Add(unhiddenIndices[randomIndex]);
            unhiddenIndices.RemoveAt(randomIndex);
        }

        return indices;
    }
}
