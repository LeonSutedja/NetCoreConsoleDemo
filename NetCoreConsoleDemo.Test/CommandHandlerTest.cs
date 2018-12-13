using NetCoreConsoleDemo.Infrastructure.Bootstrapper;
using NetCoreConsoleDemo.Infrastructure.CommandHandler;
using Pressius;
using Shouldly;
using System;
using System.Collections.Generic;
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

        public static IEnumerable<object[]> DefaultSampleCommand()
        {
            var pressiusInputs = Permutor.Generate<SampleCommand>();
            foreach (var input in pressiusInputs)
            {
                yield return new object[]
                {
                    input.Id, input.Name, input.AccountNo
                };
            }
        }        

        [Theory]
        [MemberData("DefaultSampleCommand")]
        public void SampleCommand_ShouldRunWithNoException(string id, string name, string accountno)
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
        [InlineData("1234", null, "109387265")]
        public void SampleCommand_ShouldShowExceptionInConsole(string id, string name, string accountno)
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
        [MemberData("DefaultSampleCommand")]
        public void SampleCommandWithAsyncException_ShouldRunWithNoException(string id, string name, string accountno)
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
        [InlineData("1234", "Pythagoras", "32198765")]
        [InlineData("1234", "Mark Smith", "109387265")]
        public void SampleCommandWithAsyncException_ShouldShowAsyncExceptionInConsole(string id, string name, string accountno)
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
        [InlineData("1234", null, "32198765")]
        public void SampleCommandWithAsyncException_ShouldShowNormalExceptionInConsole(string id, string name, string accountno)
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