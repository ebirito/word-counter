namespace WordCounter.Core
{
    public static class WordCounter
    {
        private static readonly string[] WordDelimiters = new string[] { Environment.NewLine, ".", "?", "!", " ", ";", ":", "," };

        public static IEnumerable<WordCount> CountWords(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return Enumerable.Empty<WordCount>();
            }

            string[] words = input.Split(WordDelimiters, StringSplitOptions.RemoveEmptyEntries);
            return words.GroupBy(word => word.ToLower()).Select(
                wordGroup => new WordCount { Word = wordGroup.Key, Count = wordGroup.Count() }).
                OrderByDescending(wordCount => wordCount.Count).ThenBy(wordCount => wordCount.Word).ToList();
        }
    }
}