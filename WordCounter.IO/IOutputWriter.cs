using WordCounter.Core;

namespace WordCounter.IO
{
    public interface IOutputWriter
    {
        Task WriteOutput(IEnumerable<WordCount> wordCounts);
    }
}
