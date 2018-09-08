using System;

namespace NetCoreConsoleDemo
{
    internal class Program
    {
        private static ICommandHandlerFactory CommandHandlerFactory { get; set; }

        private static void Main(string[] args)
        {
            // Get command handler and run
            AutofacContainer.Initiate();
            CommandHandlerFactory = AutofacContainer.Resolve<ICommandHandlerFactory>();
            var sampleCommandHandler = CommandHandlerFactory.GetCommandHandler<SampleCommand>();
            var sampleSuccessCommand = new SampleCommand
            {
                Id = 1,
                AccountNo = "12345",
                Name = "John Smith"
            };
            sampleCommandHandler.Handle(sampleSuccessCommand);

            var sampleFailedCommand = new SampleCommand
            {
                Id = 1,
                AccountNo = null,
                Name = null
            };
            sampleCommandHandler.Handle(sampleFailedCommand);

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}