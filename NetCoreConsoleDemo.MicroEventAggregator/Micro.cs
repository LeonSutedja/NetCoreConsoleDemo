using System;
using System.Collections.Generic;
using NetCoreConsoleDemo.Infrastructure.Bootstrapper;

namespace NetCoreConsoleDemo.MicroEventAggregator
{
    public class Micro
    {
        public static void Publish(IEvent ev)
        {
            var eventType = ev.GetType();
            var handlers = GetEventHandlers(eventType);

            dynamic eventCasted = Convert.ChangeType(ev, eventType);
            foreach (var handler in handlers)
            {
                handler.Handle(eventCasted);
            }
        }

        private static IEnumerable<dynamic> GetEventHandlers(Type eventType)
        {
            var eventHandlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
            var iEnumerableType = typeof(IEnumerable<>).MakeGenericType(eventHandlerType);
            return (dynamic) AutofacContainer.Resolve(iEnumerableType);
        }
    }
}
