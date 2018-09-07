namespace NetCoreConsoleDemo
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TInput, TOutput> GetCommandHandler<TInput, TOutput>();
    }
}