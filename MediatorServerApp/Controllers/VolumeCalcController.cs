using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Handlers;

namespace ServerApp.Controllers
{
    [Controller]
    public class VolumeCalcController : Controller
    {
        private readonly IMediator _mediator;

        public VolumeCalcController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/volume/calc")]
        public async Task VolumeCalc(int x, int y, int z)
        {
            var command = new CommandVolumeCalc { X = x, Y = y, Z = z };
            string answer = await _mediator.Send(command);
            await Response.WriteAsync(answer);
        }
    }
}