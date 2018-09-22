using System;

namespace NetCoreConsoleDemo
{
    public class InteractorCommand
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public Action Runner { get; set; }
    }
}