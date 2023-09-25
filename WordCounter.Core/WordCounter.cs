using System.Collections.Concurrent;

namespace WordCounter.Core
{
    public static class WordCounter
    {
        private static readonly string[] WordDelimiters = new string[] { Environment.NewLine, ".", "?", "!", " ", ";", ":", "," };

        public static IEnumerable<WordCount> CountWords(IEnumerable<string> lines)
        {
            if (lines == null)
            {
                return Enumerable.Empty<WordCount>();
            }

            var wordCounts = new ConcurrentDictionary<string, int>();

            Parallel.ForEach(lines, line =>
            {
                if (string.IsNullOrEmpty(line))
                {
                    return;
                }
                string[] words = line.Split(WordDelimiters, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    wordCounts.AddOrUpdate(word.ToLower(), 1, (_, existingCount) => existingCount + 1);
                }
            });

            return wordCounts.Keys.Select(word => new WordCount { Word = word, Count = wordCounts[word] })
                .OrderByDescending(wordCount => wordCount.Count).ThenBy(wordCount => wordCount.Word);
        }
    }
}