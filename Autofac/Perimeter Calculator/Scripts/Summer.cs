namespace Application.Scripts
{
    public interface ISummer
    {
        public void InitNumbers(int a, int b);
        public int CalcSum();
    }

    public class Summer : ISummer, IDisposable
    {
        private int _a = 0;
        private int _b = 0;

        public void InitNumbers(int a, int b)
        {
            this._a = a;
            this._b = b;
        }

        public int CalcSum()
        {
            int s = this._a + this._b;
            return s;
        }

        public void Dispose()
        {
            Console.WriteLine($"Dispose 'Summer' with fields {this._a} and {this._b}");
        }
    }
}