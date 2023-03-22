using MediatR;

namespace ServerApp.Handlers
{
    public class CommandVolumeCalc : IRequest<string>
    {
        public int X { get; init; } = 0;
        public int Y { get; init; } = 0;
        public int Z { get; init; } = 0;
    }
    
    public class VolumeCalcHandler : IRequestHandler<CommandVolumeCalc, string>
    {
        public Task<string> Handle(CommandVolumeCalc request, CancellationToken cancellationToken)
        {
            int m = request.X * request.Y * request.Z;
            string result = $"{request.X} * {request.Y} * {request.Z} = {m}";
            return Task.FromResult(result);
        }
    }
}