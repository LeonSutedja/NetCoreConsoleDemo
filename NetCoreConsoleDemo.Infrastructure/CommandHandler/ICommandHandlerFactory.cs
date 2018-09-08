namespace NetCoreConsoleDemo.Infrastructure.CommandHandler
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TInput> GetCommandHandler<TInput>();
    }
}