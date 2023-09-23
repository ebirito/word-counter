using WordCounter.Core;
using WordCounter.Logging;

namespace WordCounter.IO
{
    public class FileOutputWriter : IOutputWriter
    {
        public FileOutputWriter(string outputFilePath, Func<WordCount, string> fileOutputLineFormat, ILogger logger)
        {
            this.OutputFilePath = outputFilePath;
            this.FileOutputLineFormat = fileOutputLineFormat;
            Logger = logger;

        }

        public string OutputFilePath { get; private set; }
        public Func<WordCount, string>? FileOutputLineFormat { get; private set; }
        private ILogger Logger;

        public async Task WriteOutput(IEnumerable<WordCount> wordCounts)
        {
            if (string.IsNullOrEmpty(this.OutputFilePath))
            {
                await this.Logger.LogMessage("Ouput file path not provided", LogSeverity.Fatal);
                throw new ArgumentNullException(nameof(this.OutputFilePath));
            }
            if (this.FileOutputLineFormat == null)
            {
                await this.Logger.LogMessage("Output line format not provided", LogSeverity.Fatal);
                throw new ArgumentNullException(nameof(this.FileOutputLineFormat));
            }

            using (StreamWriter outputFile = new StreamWriter(this.OutputFilePath))
            {
                foreach (WordCount wordCount in wordCounts)
                {
                    await outputFile.WriteLineAsync(this.FileOutputLineFormat(wordCount));
                }
            }
        }
    }
}
