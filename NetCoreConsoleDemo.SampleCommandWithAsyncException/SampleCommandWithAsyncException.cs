using System;
using System.Threading.Tasks;
using NetCoreConsoleDemo.Infrastructure.CommandHandler;
using NetCoreConsoleDemo.Infrastructure.Configuration;
using NetCoreConsoleDemo.MicroEventAggregator;

namespace NetCoreConsoleDemo.SampleCommandWithAsyncException
{
    public class SampleCommandWithAsyncException
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountNo { get; set; }
    }

    public class SampleCommandWithAsyncExceptionCommandHandler : ICommandHandler<SampleCommandWithAsyncException>
    {
        private readonly IConfiguration _config;

        public SampleCommandWithAsyncExceptionCommandHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task Handle(SampleCommandWithAsyncException model)
        {
            // This will trigger error if there is null on the name
            var modelName = model.Name.ToString();

            var connectionString = _config.GetConfig("ConnectionString");
            var commandToRun = _config.GetConfig("CommandToRun");
            Console.WriteLine("Connection String: {0}, command: {1}", connectionString, commandToRun);

            await RealAsyncTask(model);
        }

        private async Task RealAsyncTask(SampleCommandWithAsyncException model)
        {
            Console.WriteLine("Start time: {0}", DateTime.Now.ToString());
            await Task.Delay(5000);
            throw new Exception("Exception in Async");
        }
    }

    public class SampleCommandHandledEventHandler : IEventHandler<SampleCommandHandledEvent>
    {
        public async Task Handle(SampleCommandHandledEvent model)
        {
            await Task.Delay(1000);
            Console.WriteLine("SampleCommandHandled 1, ID: {0} in Async Exception", model.SampleCommandHandled.Id);
        }
    }

    public class SampleCommandHandled2EventHandler : IEventHandler<SampleCommandHandledEvent>
    {
        public async Task Handle(SampleCommandHandledEvent model)
        {
            await Task.Delay(1000);
            Console.WriteLine("SampleCommandHandled 2, ID: {0} in Async Exception", model.SampleCommandHandled.Id);
        }
    }

    public class SampleCommandHandled3EventHandler : IEventHandler<SampleCommandHandledEvent>
    {
        public async Task Handle(SampleCommandHandledEvent model)
        {
            await Task.Delay(1000);

            Console.WriteLine("SampleCommandHandled 3, ID: {0} in Async Exception", model.SampleCommandHandled.Id);
        }
    }
}
