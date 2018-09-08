using NetCoreConsoleDemo.Infrastructure.Bootstrapper;
using NetCoreConsoleDemo.Infrastructure.CommandHandler;
using Shouldly;
using System;
using System.IO;
using Xunit;

namespace NetCoreConsoleDemo.Test
{
    public class CommandHandlerTest
    {
        private ICommandHandlerFactory CommandHandlerFactory { get; }

        public CommandHandlerTest()
        {
            AutofacContainer.Initiate();
            CommandHandlerFactory = (ICommandHandlerFactory)AutofacContainer.Resolve(typeof(ICommandHandlerFactory));
        }

        [Theory]
        [InlineData(1, "Pythagoras", "32198765")]
        [InlineData(2, "Mark Smith", "109387265")]
        [InlineData(2, null, "109387265")]
        [InlineData(2, "Mark Smith", null)]
        public void SampleCommand_ShouldRunWithNoException(int id, string name, string accountno)
        {
            var handler = CommandHandlerFactory.GetCommandHandler<SampleCommand>();
            var command = new SampleCommand
            {
                Id = id,
                Name = name,
                AccountNo = accountno
            };
            Should.NotThrow(() => handler.Handle(command));
        }

        [Theory]
        [InlineData(2, null, "109387265")]
        public void SampleCommand_ShouldShowExceptionInConsole(int id, string name, string accountno)
        {
            var handler = CommandHandlerFactory.GetCommandHandler<SampleCommand>();
            var command = new SampleCommand
            {
                Id = id,
                Name = name,
                AccountNo = accountno
            };

            using (var sw = new StringWriter())
            {
                // redirect console output to stringwriter
                Console.SetOut(sw);

                Should.NotThrow(() => handler.Handle(command));

                var expected = "Exception";
                var consoleOutput = sw.ToString();
                consoleOutput.ShouldContain(expected);
            }
        }

        [Theory]
        [InlineData(1, "Pythagoras", "32198765")]
        [InlineData(2, "Mark Smith", "109387265")]
        [InlineData(2, null, "109387265")]
        [InlineData(2, "Mark Smith", null)]
        public void SampleCommandWithAsyncException_ShouldRunWithNoException(int id, string name, string accountno)
        {
            var handler = CommandHandlerFactory.GetCommandHandler<SampleCommandWithAsyncException.SampleCommandWithAsyncException>();
            var command = new SampleCommandWithAsyncException.SampleCommandWithAsyncException
            {
                Id = id,
                Name = name,
                AccountNo = accountno
            };
            Should.NotThrow(() => handler.Handle(command));
        }

        [Theory]
        [InlineData(1, "Pythagoras", "32198765")]
        [InlineData(2, "Mark Smith", "109387265")]
        public void SampleCommandWithAsyncException_ShouldShowAsyncExceptionInConsole(int id, string name, string accountno)
        {
            var handler = CommandHandlerFactory.GetCommandHandler<SampleCommandWithAsyncException.SampleCommandWithAsyncException>();
            var command = new SampleCommandWithAsyncException.SampleCommandWithAsyncException
            {
                Id = id,
                Name = name,
                AccountNo = accountno
            };

            using (var sw = new StringWriter())
            {
                // redirect console output to stringwriter
                Console.SetOut(sw);

                Should.CompleteIn(() => handler.Handle(command), TimeSpan.FromSeconds(11));

                var expected = "Exception in Async";
                var consoleOutput = sw.ToString();
                consoleOutput.ShouldContain(expected);
            }
        }

        [Theory]
        [InlineData(1, null, "32198765")]
        public void SampleCommandWithAsyncException_ShouldShowNormalExceptionInConsole(int id, string name, string accountno)
        {
            var handler = CommandHandlerFactory.GetCommandHandler<SampleCommandWithAsyncException.SampleCommandWithAsyncException>();
            var command = new SampleCommandWithAsyncException.SampleCommandWithAsyncException
            {
                Id = id,
                Name = name,
                AccountNo = accountno
            };

            using (var sw = new StringWriter())
            {
                // redirect console output to stringwriter
                Console.SetOut(sw);

                Should.NotThrow(() => handler.Handle(command));

                var expected = "Exception";
                var consoleOutput = sw.ToString();
                consoleOutput.ShouldContain(expected);
            }
        }
    }
}