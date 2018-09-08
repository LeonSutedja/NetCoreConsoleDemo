using Serilog;
using System;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreConsoleDemo
{
    public class CommandHandlerErrorHandlingDecorator<TInput> : ICommandHandler<TInput>
    {
        private readonly ICommandHandler<TInput> _handler;
        private readonly ILogger _logger;

        public CommandHandlerErrorHandlingDecorator(
            ICommandHandler<TInput> handler,
            ILogger logger)
        {
            _handler = handler;
            _logger = logger;
        }

        public async Task Handle(TInput model)
        {
            try
            {
                await _handler.Handle(model);
            }
            catch (Exception e)
            {
                var modelSerialized = model.ToJson();
                var modelType = model.GetType().Name;
                var exceptionMessage = new StringBuilder();
                exceptionMessage.AppendFormat("EXCEPTION: {0} values: {1}{2}", modelType, modelSerialized, Environment.NewLine);
                exceptionMessage.AppendFormat("Message: {0}{1}", e.Message, Environment.NewLine);
                exceptionMessage.AppendFormat("Stacktrace: {0}", e.StackTrace);
                _logger.Fatal(exceptionMessage.ToString());
            }
        }
    }
}