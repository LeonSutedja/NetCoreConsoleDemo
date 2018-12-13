using NetCoreConsoleDemo.Infrastructure.Bootstrapper;
using NetCoreConsoleDemo.Infrastructure.CommandHandler;
using Pressius;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace NetCoreConsoleDemo.Test
{
    public class RegisterCustomerCommandTest
    {
        private ICommandHandlerFactory CommandHandlerFactory { get; }

        public RegisterCustomerCommandTest()
        {
            AutofacContainer.Initiate();
            CommandHandlerFactory = (ICommandHandlerFactory)AutofacContainer.Resolve(typeof(ICommandHandlerFactory));
        }

        public static IEnumerable<object[]> DefaultRegisterCustomerCommand()
        {
            var pressiusInputs = Permutor.Generate<RegisterCustomerCommand>();
            foreach (var input in pressiusInputs)
            {
                yield return new object[]
                {
                    input.Id, input.Name, input.AccountNo
                };
            }
        }

        [Theory]
        [MemberData("DefaultRegisterCustomerCommand")]
        public void SampleCommand_ShouldRunWithNoException(string id, string name, string accountno)
        {
            var handler = CommandHandlerFactory.GetCommandHandler<RegisterCustomerCommand>();
            var command = new RegisterCustomerCommand
            {
                Id = id,
                Name = name,
                AccountNo = accountno
            };
            Should.NotThrow(() => handler.Handle(command));
        }

    }
}
