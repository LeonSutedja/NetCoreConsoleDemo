using NetCoreConsoleDemo.Infrastructure.CommandHandler;
using NetCoreConsoleDemo.Infrastructure.Configuration;
using NetCoreConsoleDemo.MicroEventAggregator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreConsoleDemo
{
    public class CustomerOrderCommand
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public List<string> ItemsOrdered { get; set; }
    }

    public class CustomerOrderCommandHandler : ICommandHandler<CustomerOrderCommand>
    {
        private readonly IConfiguration _config;

        public CustomerOrderCommandHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task Handle(CustomerOrderCommand model)
        {
            // This will trigger error if there is null on the name
            Console.WriteLine("CustomerOrderCommand");
            await AsyncValidation(model);
        }

        private async Task AsyncValidation(CustomerOrderCommand model)
        {
            // Do some validations for this command here
            await Task.Delay(1000);
            Console.WriteLine("Handling CustomerOrderCommand - Id:{0}, Customer Id:{1}", model.Id, model.CustomerId);

            var sampleCommandHandledEvent =
                new CustomerOrdered { Command = model, TimeHandled = DateTime.Now };

            // Once validation is finished, we publish as a customer registered event.
            Micro.Publish(sampleCommandHandledEvent);
        }
    }

    public class CustomerOrdered : IEvent
    {
        public DateTime TimeHandled { get; set; }
        public CustomerOrderCommand Command { get; set; }
    }

    public class ChargeCreditCardEventHandler : IEventHandler<CustomerOrdered>
    {
        public async Task Handle(CustomerOrdered model)
        {
            var r = new Random();
            await Task.Delay(r.Next(1000, 10000));
            Console.WriteLine("Credit card charged, ID: {0}, Customer Id: {1}", model.Command.Id, model.Command.CustomerId);
        }
    }

    public class EmailSentEventHandler : IEventHandler<CustomerOrdered>
    {
        public async Task Handle(CustomerOrdered model)
        {
            var r = new Random();
            await Task.Delay(r.Next(1000, 10000));
            Console.WriteLine("Email sent, ID: {0}, Customer Id: {1}", model.Command.Id, model.Command.CustomerId);
        }
    }

    public class NotifyWarehouseEventHandler : IEventHandler<CustomerOrdered>
    {
        public async Task Handle(CustomerOrdered model)
        {
            var r = new Random();
            await Task.Delay(r.Next(1000, 10000));
            Console.WriteLine("Warehouse notified, ID: {0}, Customer Id: {1}", model.Command.Id, model.Command.CustomerId);
        }
    }

    public class PersistToDatabaseEventHandler : IEventHandler<CustomerOrdered>
    {
        public async Task Handle(CustomerOrdered model)
        {
            var r = new Random();
            await Task.Delay(r.Next(1000, 10000));
            Console.WriteLine("Persisted, ID: {0}, Customer Id: {1}", model.Command.Id, model.Command.CustomerId);
        }
    }
}
