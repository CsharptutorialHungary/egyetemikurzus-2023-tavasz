namespace GoogleBooks.ViewCommon;
public static class CommandHelper
{
    public static string FindMostSimilarWord(List<string> words, string target)
    {
        string mostSimilarWord = "";
        int bestScore = 0;

        foreach (string word in words)
        {
            int score = CalculateSimilarityScore(word, target);

            if (score > bestScore)
            {
                bestScore = score;
                mostSimilarWord = word;
            }
        }

        return bestScore > 2 ? mostSimilarWord : "";
    }

    private static int CalculateSimilarityScore(string word1, string word2)
    {
        int score = 0;

        for (int i = 0; i < Math.Min(word1.Length, word2.Length); i++)
        {
            if (word1[i] == word2[i])
            {
                score++;
            }
        }

        return score;
    }
}
