using System;
using System.Threading.Tasks;
using Serilog;

namespace NetCoreConsoleDemo
{
    public class SampleCommandWithAsyncException
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountNo { get; set; }
    }

    public class SampleCommandWithAsyncExceptionCommandHandler : ICommandHandler<SampleCommand>
    {
        private readonly IConfiguration _config;

        public SampleCommandWithAsyncExceptionCommandHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task Handle(SampleCommand model)
        {
            // This will trigger error if there is null on the name
            var modelName = model.Name.ToString();

            var connectionString = _config.AppSettings["ConnectionString"];
            var commandToRun = _config.AppSettings["CommandToRun"];
            Console.WriteLine("Connection String: {0}, command: {1}", connectionString, commandToRun);

            await RealAsyncTask(model);
        }

        private async Task RealAsyncTask(SampleCommand model)
        {
            Console.WriteLine("Start time: {0}", DateTime.Now.ToString());
            await Task.Delay(1000);
            throw new Exception("Exception in Async");
        }
    }
}