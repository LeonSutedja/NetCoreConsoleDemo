using NetCoreConsoleDemo.MicroEventAggregator.Extensions;
using Serilog;
using System.Threading.Tasks;

namespace NetCoreConsoleDemo.MicroEventAggregator
{
    public class EventHandlerLoggerDecorator<TInput> : IEventHandler<TInput>
    {
        private readonly IEventHandler<TInput> _handler;
        private readonly ILogger _logger;

        public EventHandlerLoggerDecorator(
            IEventHandler<TInput> handler,
            ILogger logger)
        {
            _handler = handler;
            _logger = logger;
        }

        public Task Handle(TInput model)
        {
            // Log the command here
            var watch = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here
            var task = _handler.Handle(model);
            watch.Stop();

            var elapsedMs = watch.ElapsedMilliseconds;
            var modelSerialized = model.ToJson();
            var modelType = model.GetType().Name;

            _logger.Information("Processed Event {0} for {1} ms - values: {2}",
                modelType,
                elapsedMs,
                modelSerialized);
            return task;
        }
    }
}