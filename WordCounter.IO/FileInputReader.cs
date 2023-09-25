using WordCounter.Logging;

namespace WordCounter.IO
{
    public class FileInputReader : IInputReader
    {
        public FileInputReader(string inputFilePath, ILogger logger)
        {
            this.InputFilePath = inputFilePath;
            this.Logger = logger;
        }

        public string InputFilePath { get; private set; }

        private ILogger Logger;

        public async Task<IEnumerable<string>> ReadInput()
        {
            if (string.IsNullOrEmpty(this.InputFilePath))
            {
                await this.Logger.LogMessage("Input file path not provided", LogSeverity.Fatal);
                throw new ArgumentNullException(nameof(this.InputFilePath));
            }
            if(!File.Exists(this.InputFilePath))
            {
                await this.Logger.LogMessage("Input file path points to a file that can not be found", LogSeverity.Fatal);
                throw new InvalidOperationException(nameof(this.InputFilePath));
            }

            return File.ReadLines(this.InputFilePath);
        }
    }
}