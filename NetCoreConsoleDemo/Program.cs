using NetCoreConsoleDemo.Infrastructure.Bootstrapper;
using NetCoreConsoleDemo.Infrastructure.CommandHandler;
using System.Collections.Generic;

namespace NetCoreConsoleDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Get command handler and run
            AutofacContainer.Initiate();            

            var crdSampleCmd = new InteractorCommand
            {
                Key = "1",
                Description = "Run Sample Command",
                Runner = () =>
                {
                    var cmdList = new List<SampleCommand>();
                    var cmd = new SampleCommand
                    {
                        Id = RandomGeneratorExtension.GenerateRandomId(),
                        AccountNo = "12345",
                        Name = "John Smith"
                    };
                    cmdList.Add(cmd);
                    FireAndForgetCommands(cmdList);
                }
            };

            var crdSampleFailedCmd = new InteractorCommand
            {
                Key = "2",
                Description = "Run Sample Failed Command with null as name",
                Runner = () =>
                {
                    var cmdList = new List<SampleCommand>();
                    var cmd = new SampleCommand
                    {
                        Id = RandomGeneratorExtension.GenerateRandomId(),
                        AccountNo = "12345",
                        Name = null
                    };
                    cmdList.Add(cmd);
                    FireAndForgetCommands(cmdList);
                }
            };

            var crdSampleAsyncExceptionCmd = new InteractorCommand
            {
                Key = "3",
                Description = "Run Sample Failed Async Command with null as name",
                Runner = () =>
                {
                    var cmdList = new List<SampleCommandWithAsyncException.SampleCommandWithAsyncException>();
                    var cmd = new SampleCommandWithAsyncException.SampleCommandWithAsyncException
                    {
                        Id = RandomGeneratorExtension.GenerateRandomId(),
                        AccountNo = "12345",
                        Name = "Blue Jay"
                    };
                    cmdList.Add(cmd);
                    FireAndForgetCommands(cmdList);
                }
            };

            var registerCustomerCommand = new InteractorCommand
            {
                Key = "4",
                Description = "Run Register Customer Command",
                Runner = () =>
                {
                    var cmdList = new List<RegisterCustomerCommand>();
                    var cmd = new RegisterCustomerCommand
                    {
                        Id = RandomGeneratorExtension.GenerateRandomId(),
                        AccountNo = "55132",
                        Name = "Adam Smith"
                    };
                    cmdList.Add(cmd);
                    FireAndForgetCommands(cmdList);
                }
            };

            var customerOrderCommand = new InteractorCommand
            {
                Key = "5",
                Description = "Run Customer Order Command",
                Runner = () =>
                {
                    var cmdList = new List<CustomerOrderCommand>();
                    var cmd = new CustomerOrderCommand
                    {
                        Id = RandomGeneratorExtension.GenerateRandomId(),
                        CustomerId = RandomGeneratorExtension.GenerateRandomId(),
                        ItemsOrdered = new List<string>() { "Transformer 1", "Dinobot 2" }
                    };
                    cmdList.Add(cmd);
                    FireAndForgetCommands(cmdList);
                }
            };

            var cmdRunner = new UserInteractor();
            cmdRunner.AddCommandRunner(crdSampleCmd);
            cmdRunner.AddCommandRunner(crdSampleFailedCmd);
            cmdRunner.AddCommandRunner(crdSampleAsyncExceptionCmd);
            cmdRunner.AddCommandRunner(registerCustomerCommand);
            cmdRunner.AddCommandRunner(customerOrderCommand);
            cmdRunner.Start();
        }

        private static void FireAndForgetCommands<T>(List<T> commands)
        {
            var factory = AutofacContainer.Resolve<ICommandHandlerFactory>();
            var sampleCommandHandler = factory.GetCommandHandler<T>();
            commands.ForEach(async cmd =>
            {
                await sampleCommandHandler.Handle(cmd);
            });
        }
    }
}