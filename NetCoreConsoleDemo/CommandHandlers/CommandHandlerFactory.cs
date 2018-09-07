using Autofac;

namespace NetCoreConsoleDemo
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        public ICommandHandler<TInput, TOutput> GetCommandHandler<TInput, TOutput>()
            => (ICommandHandler<TInput, TOutput>)
                AutofacContainer.Container.Resolve(
                typeof(ICommandHandler<TInput, TOutput>));
    }
}