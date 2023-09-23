using FluentAssertions;

namespace WordCounter.Core.Test
{
    public class WordCounterTests
    {
        private static readonly string[] _emptyStrings = { null, string.Empty };
        private static readonly string[] _wordDelimiters = { Environment.NewLine, ".", "?", "!", " ", ";", ":", "," };


        [TestCaseSource(nameof(_emptyStrings))]
        [TestCaseSource(nameof(_wordDelimiters))]
        public void CountWords_WhenInputDoesNotContainAnyNonDelimiterCharacters_ShouldReturnEmptyList(string input)
        {
            // Arrange
            var expectedWordCounts = new List<WordCount>();

            // Act
            var actualWordCounts = WordCounter.CountWords(input);

            // Assert
            this.AssertWordCounts(expectedWordCounts, actualWordCounts);
        }

        [TestCaseSource(nameof(_wordDelimiters))]
        public void CountWords_TestDelimiters(string delimiter)
        {
            // Arrange
            string input = $"testing{delimiter}testing";
            var expectedWordCounts = new List<WordCount>() {
                new WordCount { Word = "testing", Count = 2 }
            };

            // Act
            var actualWordCounts = WordCounter.CountWords(input);

            // Assert
            this.AssertWordCounts(expectedWordCounts, actualWordCounts);
        }

        [Test]
        public void CountWords_WhenSingleLinePassed_ShouldCountWordsSuccesfully()
        {
            // Arrange
            string input = "Historically, the world of data and the world of objects";
            var expectedWordCounts = new List<WordCount>() { 
                new WordCount { Word = "of", Count = 2 },
                new WordCount { Word = "the", Count = 2 },
                new WordCount { Word = "world", Count = 2 },
                new WordCount { Word = "and", Count = 1 },
                new WordCount { Word = "data", Count = 1 },
                new WordCount { Word = "historically", Count = 1 },
                new WordCount { Word = "objects", Count = 1 }
            };

            // Act
            var actualWordCounts = WordCounter.CountWords(input);

            // Assert
            this.AssertWordCounts(expectedWordCounts, actualWordCounts);
        }

        [Test]
        public void CountWords_WhenMultipleLinePassed_ShouldCountWordsSuccesfully()
        {
            // Arrange
            string input = "Historically, the world of data " + Environment.NewLine +
                            "and the world of objects" + Environment.NewLine +
                            "are the best? Debatable.";
            var expectedWordCounts = new List<WordCount>() {
                new WordCount { Word = "the", Count = 3 },
                new WordCount { Word = "of", Count = 2 },
                new WordCount { Word = "world", Count = 2 },
                new WordCount { Word = "and", Count = 1 },
                new WordCount { Word = "are", Count = 1 },
                new WordCount { Word = "best", Count = 1 },
                new WordCount { Word = "data", Count = 1 },
                new WordCount { Word = "debatable", Count = 1 },
                new WordCount { Word = "historically", Count = 1 },
                new WordCount { Word = "objects", Count = 1 }
            };

            // Act
            var actualWordCounts = WordCounter.CountWords(input);

            // Assert
            this.AssertWordCounts(expectedWordCounts, actualWordCounts);
        }

        [Test]
        [TestCase("book", "BOOK")]
        [TestCase("BOOK", "book")]
        [TestCase("book", "Book")]
        public void CountWords_WhenWordsHaveDifferentCasings_ShouldCountThemAsTheSameWord(string word, string wordWithDifferentCasing)
        {
            // Arrange
            string input = $"{word} {wordWithDifferentCasing}";
            var expectedWordCounts = new List<WordCount>() {
                new WordCount { Word = word.ToLower(), Count = 2 }
            };

            // Act
            var actualWordCounts = WordCounter.CountWords(input);

            // Assert
            this.AssertWordCounts(expectedWordCounts, actualWordCounts);
        }

        private void AssertWordCounts(IEnumerable<WordCount> expectedWordCounts, IEnumerable<WordCount> actualWordCounts)
        {
            actualWordCounts.Count().Should().Be(expectedWordCounts.Count());
            for (int i = 0; i < expectedWordCounts.Count(); i++)
            {
                actualWordCounts.ElementAt(i).Should().Be(expectedWordCounts.ElementAt(i));
            }
        }
    }
}