namespace MathApi.Services
{
    public interface ISquareCalculator
    {
        public int CalcSquare(int sideA, int sideB);
    }
    
    public class SquareCalculator : ISquareCalculator
    {
        public int CalcSquare(int sideA, int sideB)
        {
            int square = sideA * sideB;
            return square;
        }
    }
}
