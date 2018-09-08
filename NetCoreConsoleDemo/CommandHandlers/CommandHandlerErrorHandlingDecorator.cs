using Serilog;
using System;

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

        public CommandResponse Handle(TInput model)
        {
            try
            {
                return _handler.Handle(model);
            }
            catch (Exception e)
            {
                var modelSerialized = model.ToJson();
                var modelType = model.GetType().Name;
                _logger.Fatal("EXCEPTION: {0} values: {1}",
                    modelType,
                    modelSerialized);
                _logger.Fatal("Message: {0}", e.Message);
                _logger.Fatal("Stacktrace: {0}", e.StackTrace);
                return CommandResponse.Failed;
            }
        }
    }
}