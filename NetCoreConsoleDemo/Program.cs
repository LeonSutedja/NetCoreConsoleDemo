using System;
using System.IO;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace NetCoreConsoleDemo
{
    internal class Program
    {
        private static ICommandHandlerFactory CommandHandlerFactory { get; set; }

        private static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var connectionString = configuration["ConnectionString"];
            Console.WriteLine("Connection String: {0}", connectionString);

            var commandToRun = configuration["CommandToRun"];
            Console.WriteLine("Command to Run: {0}", commandToRun);

            // Get command handler and run
            AutofacContainer.Initiate();
            CommandHandlerFactory = (ICommandHandlerFactory)AutofacContainer.Container.Resolve(typeof(ICommandHandlerFactory));
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