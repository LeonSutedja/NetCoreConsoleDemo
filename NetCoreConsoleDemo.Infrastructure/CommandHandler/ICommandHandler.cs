using System.Threading.Tasks;

namespace NetCoreConsoleDemo.Infrastructure.CommandHandler
{
    public interface ICommandHandler<TInput>
    {
        Task Handle(TInput model);
    }
}