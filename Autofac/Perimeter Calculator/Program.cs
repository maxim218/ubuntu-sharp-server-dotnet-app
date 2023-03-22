using Application.Scripts;
using Autofac;

namespace Application
{
    public static class MainProgram
    {
        public static void Main(string[] args)
        {
            // containers registration
            var builder = new ContainerBuilder();
            builder.RegisterType<Summer>().As<ISummer>();
            builder.RegisterType<ReaderConsole>().As<IReaderConsole>();
            builder.RegisterType<MullerTwo>().As<IMullerTwo>();
            builder.RegisterType<PerimeterCalculator>().As<IPerimeterCalculator>();
            var container = builder.Build();
            
            // main program
            using (var scope = container.BeginLifetimeScope())
            {
                var readerConsole = scope.Resolve<IReaderConsole>();
                int a = readerConsole.ReadNumber("Input A:");
                int b = readerConsole.ReadNumber("Input B:");
                
                var calculator = scope.Resolve<IPerimeterCalculator>();
                int p = calculator.CalcPerimeter(a, b);
                Console.WriteLine("Perimeter: " + p + '\n');
            }
        }
    }
}