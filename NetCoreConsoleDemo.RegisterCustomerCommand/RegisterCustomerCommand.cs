using NetCoreConsoleDemo.Infrastructure.CommandHandler;
using NetCoreConsoleDemo.Infrastructure.Configuration;
using NetCoreConsoleDemo.MicroEventAggregator;
using System;
using System.Threading.Tasks;

namespace NetCoreConsoleDemo
{
    public class RegisterCustomerCommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AccountNo { get; set; }
    }

    public class SampleCommandHandler : ICommandHandler<RegisterCustomerCommand>
    {
        private readonly IConfiguration _config;

        public SampleCommandHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task Handle(RegisterCustomerCommand model)
        {
            // This will trigger error if there is null on the name
            var modelName = model.Name.ToString();

            var connectionString = _config.GetConfig("ConnectionString");
            Console.WriteLine("Connection String: {0}, command: RegisterCustomerCommand", connectionString);

            await AsyncValidation(model);
        }

        private async Task AsyncValidation(RegisterCustomerCommand model)
        {
            // Do some validations for this command here
            await Task.Delay(1000);
            Console.WriteLine("Handling Command - Id:{0}, Name:{1}, AccountNo:{2}", model.Id, model.Name, model.AccountNo);

            var sampleCommandHandledEvent =
                new CustomerRegistered { Command = model, TimeHandled = DateTime.Now };

            // Once validation is finished, we publish as a customer registered event.
            Micro.Publish(sampleCommandHandledEvent);
        }
    }

    public class CustomerRegistered : IEvent
    {
        public DateTime TimeHandled { get; set; }
        public RegisterCustomerCommand Command { get; set; }
    }

    public class CustomerRegisteredNotifyExternalServiceEventHandler : IEventHandler<CustomerRegistered>
    {
        public async Task Handle(CustomerRegistered model)
        {
            var r = new Random();
            await Task.Delay(r.Next(1000, 10000));
            Console.WriteLine("External Service Notified, ID: {0}, Name: {1}", model.Command.Id, model.Command.Name);
        }
    }

    public class CustomerRegisteredPersistEventHandler : IEventHandler<CustomerRegistered>
    {
        public async Task Handle(CustomerRegistered model)
        {
            var r = new Random();
            await Task.Delay(r.Next(1000, 10000));
            Console.WriteLine("Persisted, ID: {0}, Name: {1}", model.Command.Id, model.Command.Name);
        }
    }

    public class CustomerRegisteredNotifyInternalServiceEventHandler : IEventHandler<CustomerRegistered>
    {
        public async Task Handle(CustomerRegistered model)
        {
            var r = new Random();
            await Task.Delay(r.Next(1000, 10000));
            Console.WriteLine("Internal Service Notified, ID: {0}, Name: {1}", model.Command.Id, model.Command.Name);
        }
    }



    public class CustomerRegisteredPasswordCreationEventHandler : IEventHandler<CustomerRegistered>
    {
        public async Task Handle(CustomerRegistered model)
        {
            var r = new Random();
            await Task.Delay(r.Next(1000, 10000));
            Console.WriteLine("Password Created, ID: {0}, Name: {1}", model.Command.Id, model.Command.Name);
            var customerPasswordCreated = new CustomerPasswordCreated
            {
                TimeHandled = DateTime.Now,
                Command = model
            };
            Micro.Publish(customerPasswordCreated);
        }
    }

    public class CustomerPasswordCreated : IEvent
    {
        public DateTime TimeHandled { get; set; }
        public CustomerRegistered Command { get; set; }
    }

    public class CustomerPasswordCreatedSendEmailEventHandler : IEventHandler<CustomerPasswordCreated>
    {
        public async Task Handle(CustomerPasswordCreated model)
        {
            var r = new Random();
            await Task.Delay(r.Next(1000, 10000));
            Console.WriteLine("Password created email sent, ID: {0}, Name: {1}", model.Command.Command.Id, model.Command.Command.Name);

        }
    }

    public class CustomerPasswordCreatedActivateAccountEventHandler : IEventHandler<CustomerPasswordCreated>
    {
        public async Task Handle(CustomerPasswordCreated model)
        {
            var r = new Random();
            await Task.Delay(r.Next(1000, 10000));
            Console.WriteLine("Password created account activated, ID: {0}, Name: {1}", model.Command.Command.Id, model.Command.Command.Name);
        }
    }
}
