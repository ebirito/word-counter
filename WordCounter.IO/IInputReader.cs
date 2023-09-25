namespace WordCounter.IO
{
    public interface IInputReader
    {
        Task<IEnumerable<string>> ReadInput();
    }
}
