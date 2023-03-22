namespace Application.Scripts
{
    public interface IMullerTwo
    {
        public void InitValue(int x);
        public int CalcMulTwo();
    }
    
    public class MullerTwo : IMullerTwo, IDisposable
    {
        private int _x = 0;

        public void InitValue(int x)
        {
            this._x = x;
        }

        public int CalcMulTwo()
        {
            int result = this._x * 2;
            return result;
        }

        public void Dispose()
        {
            Console.WriteLine($"Dispose 'MullerTwo' with field {this._x}");
        }
    }
}