namespace NetCoreConsoleDemo
{
    public interface ICommandHandler<TInput>
    {
        void Handle(TInput model);
    }
}