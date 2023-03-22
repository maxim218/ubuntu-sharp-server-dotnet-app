using MediatR;

namespace ServerApp.Handlers
{
    public class CommandSumCalc : IRequest<string>
    {
        public int A { get; init; } = 0;
        public int B { get; init; } = 0;
    }
    
    public class SumCalcHandler : IRequestHandler<CommandSumCalc, string>
    {
        public Task<string> Handle(CommandSumCalc request, CancellationToken cancellationToken)
        {
            int s = request.A + request.B;
            string result = $"{request.A} + {request.B} = {s}";
            return Task.FromResult(result);
        }
    }
}