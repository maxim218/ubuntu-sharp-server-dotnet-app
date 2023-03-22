namespace Application.Scripts
{
    public interface IPerimeterCalculator
    {
        public int CalcPerimeter(int sideA, int sideB);
    }
    
    public class PerimeterCalculator : IPerimeterCalculator, IDisposable
    {
        private ISummer _summerObj;
        private IMullerTwo _mullerTwoObj;

        public PerimeterCalculator(ISummer summerObjParam, IMullerTwo mullerTwoObjParam)
        {
            this._summerObj = summerObjParam;
            this._mullerTwoObj = mullerTwoObjParam;
        }

        public int CalcPerimeter(int sideA, int sideB)
        {
            this._summerObj.InitNumbers(sideA, sideB);
            int sum = this._summerObj.CalcSum();
            this._mullerTwoObj.InitValue(sum);
            int per = this._mullerTwoObj.CalcMulTwo();
            return per;
        }

        public void Dispose()
        {
            Console.WriteLine($"Dispose 'PerimeterCalculator' object");
        }
    }
}
