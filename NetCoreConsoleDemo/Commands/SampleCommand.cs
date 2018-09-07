using System;

namespace NetCoreConsoleDemo
{
    public class SampleCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountNo { get; set; }
    }

    public class SampleCommandHandler : ICommandHandler<SampleCommand, bool>
    {
        public bool Handle(SampleCommand model)
        {
            Console.WriteLine("Handling Sample Command - Id:{0}, Name:{1}, AccountNo:{2}", model.Id, model.Name, model.AccountNo);
            return true;
        }
    }
}