using System.Threading.Tasks;

namespace NetCoreConsoleDemo
{
    public interface ICommandHandler<TInput>
    {
        Task Handle(TInput model);
    }
}