using System;
using Autofac;

namespace NetCoreConsoleDemo
{
    internal class Program
    {
        private static ICommandHandlerFactory _commandHandlerFactory { get; set; }

        private static void Main(string[] args)
        {
            AutofacContainer.Initiate();
            _commandHandlerFactory = (ICommandHandlerFactory)AutofacContainer.Container.Resolve(typeof(ICommandHandlerFactory));
            var sampleCommandHandler = _commandHandlerFactory.GetCommandHandler<SampleCommand, bool>();
            var sampleCommand = new SampleCommand()
            {
                Id = 1,
                AccountNo = "1234578",
                Name = "Mark"
            };
            var issuccess = sampleCommandHandler.Handle(sampleCommand);
          
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}