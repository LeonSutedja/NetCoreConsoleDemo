using System;
using System.Collections.Generic;

namespace NetCoreConsoleDemo
{
    internal class Program
    {
        private static ICommandHandlerFactory CommandHandlerFactory { get; set; }

        private static void Main(string[] args)
        {
            // Get command handler and run
            AutofacContainer.Initiate();
            var sampleCommands = new List<SampleCommand>();

            var sampleSuccessCommand = new SampleCommand
            {
                Id = 1,
                AccountNo = "12345",
                Name = "John Smith"
            };
            sampleCommands.Add(sampleSuccessCommand);

            var sampleSuccessCommand2 = new SampleCommand
            {
                Id = 2,
                AccountNo = "54321",
                Name = "Jane smith"
            };
            sampleCommands.Add(sampleSuccessCommand2);

            var sampleSuccessCommand3 = new SampleCommand
            {
                Id = 4,
                AccountNo = "99999",
                Name = "Jane doe"
            };
            sampleCommands.Add(sampleSuccessCommand3);

            var sampleFailedCommand = new SampleCommand
            {
                Id = 3,
                AccountNo = null,
                Name = null
            };
            sampleCommands.Add(sampleFailedCommand);

            FireAndForget(sampleCommands);

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        private static void FireAndForget(List<SampleCommand> sampleCommands)
        {
            CommandHandlerFactory = AutofacContainer.Resolve<ICommandHandlerFactory>();
            var sampleCommandHandler = CommandHandlerFactory.GetCommandHandler<SampleCommand>();
            sampleCommands.ForEach(async cmd =>
            {
                await sampleCommandHandler.Handle(cmd);
            });
        }
    }
}