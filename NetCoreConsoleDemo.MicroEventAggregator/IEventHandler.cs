using System.Threading.Tasks;

namespace NetCoreConsoleDemo.MicroEventAggregator
{
    public interface IEventHandler<in TInput>
    {
        Task Handle(TInput model);
    }
}