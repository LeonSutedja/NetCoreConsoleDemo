namespace NetCoreConsoleDemo
{
    public class CommandHandlerLoggerDecorator<TInput, TEntity> : ICommandHandler<TInput, TEntity>
    {
        private readonly ICommandHandler<TInput, TEntity> _handler;

        public CommandHandlerLoggerDecorator(ICommandHandler<TInput, TEntity> handler)
        {
            _handler = handler;
        }

        public TEntity Handle(TInput model)
        {
            // Log the command here
            var result = _handler.Handle(model);
            // Log the result here
            return result;
        }
    }
}