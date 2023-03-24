using MathApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MathApi.Controllers
{
    [Controller]
    public class CalculatorController : Controller
    {
        private readonly IPerimeterCalculator _perimeterCalculator;
        private readonly ISquareCalculator _squareCalculator;
        private readonly IHeadersSetter _headersSetter;
        
        public CalculatorController(
            IPerimeterCalculator perimeterCalculator, 
            ISquareCalculator squareCalculator, 
            IHeadersSetter headersSetter)
        {
            _perimeterCalculator = perimeterCalculator;
            _squareCalculator = squareCalculator;
            _headersSetter = headersSetter;
        }
        
        [Route("/calc/perimeter")]
        public IActionResult MethodCalcPerimeter(int a, int b)
        {
            int perimeter = _perimeterCalculator.CalcPerimeter(a, b);
            _headersSetter.SetHeaders(Response);
            return Content("Perimeter: " + perimeter);
        }

        [Route("/calc/square")]
        public IActionResult MethodCalcSquare(int a, int b)
        {
            int square = _squareCalculator.CalcSquare(a, b);
            _headersSetter.SetHeaders(Response);
            return Content("Square: " + square);
        }
    }
}
