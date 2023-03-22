using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Handlers;

namespace ServerApp.Controllers
{
    [Controller]
    public class SumCalcController : Controller
    {
        private readonly IMediator _mediator;

        public SumCalcController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Route("/sum/calc")]
        public async Task SumCalc(int a, int b)
        {
            var command = new CommandSumCalc { A = a, B = b };
            string answer = await _mediator.Send(command);
            await Response.WriteAsync(answer);
        }
    }
}