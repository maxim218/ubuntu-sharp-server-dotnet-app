namespace MathApi.Services
{
    public interface IPerimeterCalculator
    {
        public int CalcPerimeter(int sideA, int sideB);
    }
    
    public class PerimeterCalculator : IPerimeterCalculator
    {
        public int CalcPerimeter(int sideA, int sideB)
        {
            int s = sideA + sideB;
            int p = s * 2;
            return p;
        }
    }
}
