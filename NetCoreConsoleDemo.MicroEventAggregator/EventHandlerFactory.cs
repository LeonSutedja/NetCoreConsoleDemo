using System.Collections.Generic;
using NetCoreConsoleDemo.Infrastructure.Bootstrapper;

namespace NetCoreConsoleDemo.MicroEventAggregator
{
    public class EventHandlerFactory<TInput> : IEventHandlerFactory<TInput>
    {
        public IEnumerable<IEventHandler<TInput>> GetEventHandlers()
            => (IEnumerable<IEventHandler<TInput>>)
                AutofacContainer.Resolve(typeof(IEnumerable<IEventHandler<TInput>>));
    }
}