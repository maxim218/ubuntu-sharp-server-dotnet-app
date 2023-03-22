namespace Application.Scripts
{
    public interface IReaderConsole
    {
        public int ReadNumber(string msg);
    }
    
    public class ReaderConsole : IReaderConsole, IDisposable
    {
        public int ReadNumber(string msg)
        {
            Console.WriteLine(msg);
            string s = Console.ReadLine()!;
            int n = int.Parse(s);
            return n;
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose 'ReaderConsole' object");
        }
    }
}