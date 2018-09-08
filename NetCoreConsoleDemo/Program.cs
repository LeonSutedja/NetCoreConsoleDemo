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
            CommandHandlerFactory = AutofacContainer.Resolve<CommandHandlerFactory>();
            var sampleCommandHandler = CommandHandlerFactory.GetCommandHandler<SampleCommand, bool>();
            var sampleCommand = new SampleCommand
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