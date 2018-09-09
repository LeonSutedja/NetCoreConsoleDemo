using System.Linq;
using NetCoreConsoleDemo.Infrastructure.Bootstrapper;
using NetCoreConsoleDemo.MicroEventAggregator;
using Shouldly;
using Xunit;

namespace NetCoreConsoleDemo.Test
{
    public class MicroEventAggregatorTest
    {
        public MicroEventAggregatorTest()
        {
            AutofacContainer.Initiate();
        }

        [Theory]
        [InlineData(1)]
        public void SampleTestEvent_ShouldRunWithNoException(int id)
        {
            var testEvent = new TestEvent
            {
                Id = id
            };
            Should.NotThrow(() => Micro.Publish(testEvent));
        }
    }
}