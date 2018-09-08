namespace NetCoreConsoleDemo
{
    public interface ICommandHandler<TInput> 
    {
        CommandResponse Handle(TInput model);
    }
}