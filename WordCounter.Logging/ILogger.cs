namespace WordCounter.Logging
{
    public interface ILogger
    {
        Task LogMessage(string message, LogSeverity severity);

        Task LogException(Exception exception);
    }
}