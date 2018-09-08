using NetCoreConsoleDemo.Infrastructure.Bootstrapper;

namespace NetCoreConsoleDemo.Infrastructure.CommandHandler
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        public ICommandHandler<TInput> GetCommandHandler<TInput>()
            => (ICommandHandler<TInput>)
                AutofacContainer.Resolve(typeof(ICommandHandler<TInput>));
    }
}