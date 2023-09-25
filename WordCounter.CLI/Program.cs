using WordCounter.IO;
using WordCounter.Logging;

namespace WordCounter.CLI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Welcome to WordCounter.CLI");
                Console.WriteLine("Usage: WordCounter.CLI.exe inputFilePath outputFilePath");
                return;
            }
            
            ILogger logger = new ConsoleLogger();
            IInputReader fileReader = new FileInputReader(args[0], logger);
            IOutputWriter fileWriter = new FileOutputWriter(args[1], (wordCount) => $"{wordCount.Word}, {wordCount.Count}", logger);

            try
            {
                var lines = await fileReader.ReadInput();
                var wordCounts = Core.WordCounter.CountWords(lines);
                await fileWriter.WriteOutput(wordCounts);
            }
            catch (Exception ex)
            {
                await logger.LogException(ex);
                return;
            }
        }
    }
}