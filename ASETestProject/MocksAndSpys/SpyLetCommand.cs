using System.Collections.Generic;
using ASEProgrammingLanguageEnvironment.Commands;
using ASEProgrammingLanguageEnvironment.Interpreter;

namespace ASETestProject.MocksAndSpys
{
    public class SpyLetCommand : ICommand
    {
        public ProgramInterpreter.State State;
        public void Execute(List<string> paramVals, ProgramInterpreter.State state)
        {
            var cmd = new LetCommand();
            cmd.Execute(paramVals,state);
            State = state;
        }
    }
}