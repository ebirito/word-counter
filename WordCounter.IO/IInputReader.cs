namespace WordCounter.IO
{
    public interface IInputReader
    {
        Task<string> ReadInput();
    }
}
