using System;

namespace NetCoreConsoleDemo
{
    public class SampleCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountNo { get; set; }
    }

    public class SampleCommandHandler : ICommandHandler<SampleCommand, bool>
    {
        private readonly IConfiguration _config;

        public SampleCommandHandler(IConfiguration config)
        {
            _config = config;
        }

        public bool Handle(SampleCommand model)
        {
            Console.WriteLine("Handling Sample Command - Id:{0}, Name:{1}, AccountNo:{2}", model.Id, model.Name, model.AccountNo);

            var connectionString = _config.AppSettings["ConnectionString"];
            Console.WriteLine("Connection String: {0}", connectionString);

            var commandToRun = _config.AppSettings["CommandToRun"];
            Console.WriteLine("Command to Run: {0}", commandToRun);
            return true;
        }
    }
}