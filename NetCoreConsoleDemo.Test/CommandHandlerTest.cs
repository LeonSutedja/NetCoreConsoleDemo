using System;
using Autofac;
using Shouldly;
using Xunit;

namespace NetCoreConsoleDemo.Test
{
    public class CommandHandlerTest
    {
        private ICommandHandlerFactory _commandHandlerFactory { get; set; }

        public CommandHandlerTest()
        {
            AutofacContainer.Initiate();
            _commandHandlerFactory = (ICommandHandlerFactory)AutofacContainer.Container.Resolve(typeof(ICommandHandlerFactory));
        }

        [Theory]
        [InlineData(1, "Pythagoras", "32198765")]
        [InlineData(2, "Mark Smith", "109387265")]
        public void Test1(int id, string name, string accountno)
        {
            var sampleCommandHandler = _commandHandlerFactory.GetCommandHandler<SampleCommand, bool>();
            var sampleCommand = new SampleCommand
            {
                Id = id,
                Name = name,
                AccountNo = accountno
            };
            var isSuccess = sampleCommandHandler.Handle(sampleCommand);
            isSuccess.ShouldBeTrue("Sample Command Was not successful");
        }
    }
}
