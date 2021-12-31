using System.Collections.Generic;
using ASEProgrammingLanguageEnvironment.Interpreter;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class EndLoopCmd : ICommand
    {
        public void Execute(List<string> paramVals, InterpreterState state)
        {
            state.Cursor = state.LoopAddressStack.Pop()-1;
        }
    }
}