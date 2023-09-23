namespace WordCounter.Logging
{
    public class ConsoleLogger : ILogger
    {
        public Task LogMessage(string message, LogSeverity severity)
        {
            Console.WriteLine($"{severity}: {message}");
            return Task.CompletedTask;
        }

        public Task LogException(Exception exception)
        {
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
            return Task.CompletedTask;
        }
    }
}
