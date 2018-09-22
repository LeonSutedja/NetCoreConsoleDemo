using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreConsoleDemo
{
    public class UserInteractor
    {
        private List<InteractorCommand> CommandRunnerData { get; }

        public UserInteractor()
        {
            CommandRunnerData = new List<InteractorCommand>();
        }

        public void AddCommandRunner(InteractorCommand data)
            => CommandRunnerData.Add(data);

        public void Start()
        {
            PrintHelper();
            string argument;
            const string exitKey = "x";
            while ((argument = Console.ReadLine()) != exitKey.ToLower())
            {
                var runner = CommandRunnerData.FirstOrDefault(crd => crd.Key.ToLower().Equals(argument.ToLower()));
                if (runner != null)
                {
                    runner.Runner();
                }
                else if (argument.ToLower().Equals("h"))
                {
                    PrintHelper();
                }
                else
                {
                    PrintHelper();
                }
            }
        }

        private void PrintHelper()
        {
            Console.WriteLine("x -- to exit");
            Console.WriteLine("h -- This help");
            CommandRunnerData.ForEach((data) =>
            {
                Console.WriteLine("{0} -- {1}", data.Key, data.Description);
            });
            Console.WriteLine("Please enter a value:");
        }
    }
}