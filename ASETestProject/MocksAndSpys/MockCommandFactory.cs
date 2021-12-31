using System.Collections.Generic;
using ASEProgrammingLanguageEnvironment.Interpreter;

namespace ASETestProject.MocksAndSpys
{
    public class MockCommandFactory : ICommandFactory
    {
        public Dictionary<string, Lex> cmdList { get; }

        public MockCommandFactory(Dictionary<string, Lex> commands)
        {
            cmdList = commands;
        }
    }
}