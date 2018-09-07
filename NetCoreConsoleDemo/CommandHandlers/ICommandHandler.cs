namespace NetCoreConsoleDemo
{
    public interface ICommandHandler<TInput, TOutput>
    {
        TOutput Handle(TInput model);
    }
}