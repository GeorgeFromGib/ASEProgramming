using System.Collections.Generic;
using ASEProgrammingLanguageEnvironment.Commands;
using ASEProgrammingLanguageEnvironment.Interpreter;

namespace ASETestProject.MocksAndSpys
{
    public class SpyLetCommand : ICommand
    {
        public InterpreterState State;
        public void Execute(List<string> paramVals, InterpreterState state)
        {
            var cmd = new LetCommand();
            cmd.Execute(paramVals,state);
            State = state;
        }
    }
}