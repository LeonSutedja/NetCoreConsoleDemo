namespace NetCoreConsoleDemo
{
    public class CommandResponse
    {
        public static CommandResponse Success => new CommandResponse(true);
        public static CommandResponse Failed => new CommandResponse(false);

        public bool IsSuccess { get; }

        private CommandResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}