using Serilog;

namespace NetCoreConsoleDemo
{
    public class CommandHandlerLoggerDecorator<TInput> : ICommandHandler<TInput>
    {
        private readonly ICommandHandler<TInput> _handler;
        private readonly ILogger _logger;

        public CommandHandlerLoggerDecorator(
            ICommandHandler<TInput> handler,
            ILogger logger)
        {
            _handler = handler;
            _logger = logger;
        }

        public void Handle(TInput model)
        {
            // Log the command here
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here
            _handler.Handle(model);
            watch.Stop();

            var elapsedMs = watch.ElapsedMilliseconds;
            var modelSerialized = model.ToJson();
            var modelType = model.GetType().Name;

            _logger.Information("Processed {0} for {1} ms - values: {2}",
                modelType,
                elapsedMs,
                modelSerialized);
        }
    }
}