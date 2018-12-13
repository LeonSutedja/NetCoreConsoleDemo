using System;
using System.Threading.Tasks;

namespace NetCoreConsoleDemo.MicroEventAggregator
{
    public class TestEvent : IEvent
    {
        public int Id { get; set; }
    }

    public class Test1EventHandler : IEventHandler<TestEvent>
    {
        public async Task Handle(TestEvent model)
        {
            await Task.Delay(1000);
            Console.WriteLine("Test Event Being Handled by 1");
        }
    }

    public class Test2EventHandler : IEventHandler<TestEvent>
    {
        public async Task Handle(TestEvent model)
        {
            await Task.Delay(1000);
            Console.WriteLine("Test Event Being Handled by 2");
        }
    }

    public class Test3EventHandler : IEventHandler<TestEvent>
    {
        public async Task Handle(TestEvent model)
        {
            await Task.Delay(1000);
            Console.WriteLine("Test Event Being Handled by 3");
        }
    }
}
