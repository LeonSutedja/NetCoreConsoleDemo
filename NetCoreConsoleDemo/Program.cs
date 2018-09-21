using NetCoreConsoleDemo.Infrastructure.Bootstrapper;
using NetCoreConsoleDemo.Infrastructure.CommandHandler;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetCoreConsoleDemo
{
    internal class Program
    {
        private static string GenerateRandomId()
        {
            var rn = new Random();
            var charsToUse = "AzByCxDwEvFuGtHsIrJqKpLoMnNmOlPkQjRiShTgUfVeWdXcYbZa1234567890";

            MatchEvaluator RandomChar = m => charsToUse[rn.Next(charsToUse.Length)].ToString();
            return Regex.Replace("XXXX-XXXXX", "X", RandomChar);
        }

        private static ICommandHandlerFactory CommandHandlerFactory { get; set; }

        private static void Main(string[] args)
        {
            // Get command handler and run
            AutofacContainer.Initiate();

            RunSampleCommands();
            RunSampleCommandsWithAsyncException();

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        private static void RunSampleCommands()
        {
            var sampleCommands = new List<SampleCommand>();

            var sampleSuccessCommand = new SampleCommand
            {
                Id = GenerateRandomId(),
                AccountNo = "12345",
                Name = "John Smith"
            };
            sampleCommands.Add(sampleSuccessCommand);

            var sampleSuccessCommand2 = new SampleCommand
            {
                Id = GenerateRandomId(),
                AccountNo = "54321",
                Name = "Jane smith"
            };
            sampleCommands.Add(sampleSuccessCommand2);

            var sampleSuccessCommand3 = new SampleCommand
            {
                Id = GenerateRandomId(),
                AccountNo = "99999",
                Name = "Jane doe"
            };
            sampleCommands.Add(sampleSuccessCommand3);

            var sampleFailedCommand = new SampleCommand
            {
                Id = GenerateRandomId(),
                AccountNo = "Failed Command",
                Name = null
            };
            sampleCommands.Add(sampleFailedCommand);

            FireAndForgetSampleCommands(sampleCommands);
        }

        private static void FireAndForgetSampleCommands(List<SampleCommand> sampleCommands)
        {
            CommandHandlerFactory = AutofacContainer.Resolve<ICommandHandlerFactory>();
            var sampleCommandHandler = CommandHandlerFactory.GetCommandHandler<SampleCommand>();
            sampleCommands.ForEach(async cmd =>
            {
                await sampleCommandHandler.Handle(cmd);
            });
        }

        private static void RunSampleCommandsWithAsyncException()
        {
            var sampleCommands = new List<SampleCommandWithAsyncException.SampleCommandWithAsyncException>();

            var cmd1 = new SampleCommandWithAsyncException.SampleCommandWithAsyncException
            {
                Id = GenerateRandomId(),
                AccountNo = "12345",
                Name = "John Smith"
            };
            sampleCommands.Add(cmd1);

            var sampleFailedCommand = new SampleCommandWithAsyncException.SampleCommandWithAsyncException
            {
                Id = GenerateRandomId(),
                AccountNo = "Failed Command",
                Name = null
            };
            sampleCommands.Add(sampleFailedCommand);

            FireAndForgetSampleCommandsWithAsyncException(sampleCommands);
        }

        private static void FireAndForgetSampleCommandsWithAsyncException(List<SampleCommandWithAsyncException.SampleCommandWithAsyncException> sampleCommands)
        {
            CommandHandlerFactory = AutofacContainer.Resolve<ICommandHandlerFactory>();
            var sampleCommandHandler = CommandHandlerFactory.GetCommandHandler<SampleCommandWithAsyncException.SampleCommandWithAsyncException>();
            sampleCommands.ForEach(async cmd =>
            {
                await sampleCommandHandler.Handle(cmd);
            });
        }
    }
}