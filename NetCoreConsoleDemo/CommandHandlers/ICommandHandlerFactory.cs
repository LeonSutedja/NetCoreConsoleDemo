namespace NetCoreConsoleDemo
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<TInput> GetCommandHandler<TInput>();
    }
}