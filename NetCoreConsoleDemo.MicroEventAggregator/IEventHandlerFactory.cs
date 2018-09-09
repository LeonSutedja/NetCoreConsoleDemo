using System.Collections.Generic;

namespace NetCoreConsoleDemo.MicroEventAggregator
{
    public interface IEventHandlerFactory<in TInput>
    {
        IEnumerable<IEventHandler<TInput>> GetEventHandlers();
    }
}