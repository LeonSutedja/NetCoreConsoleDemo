﻿using System;
using System.Threading.Tasks;
using NetCoreConsoleDemo.Infrastructure.CommandHandler;
using NetCoreConsoleDemo.Infrastructure.Configuration;

namespace NetCoreConsoleDemo
{
    public class SampleCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountNo { get; set; }
    }

    public class SampleCommandHandler : ICommandHandler<SampleCommand>
    {
        private readonly IConfiguration _config;

        public SampleCommandHandler(IConfiguration config)
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
            Console.WriteLine("Handling Sample Command with Async - Id:{0}, Name:{1}, AccountNo:{2}", model.Id, model.Name, model.AccountNo);
        }
    }
}