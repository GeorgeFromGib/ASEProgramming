using System;
using System.Collections.Generic;
using ASEProgrammingLanguageEnvironment.Extensions;
using ASEProgrammingLanguageEnvironment.Interpreter;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class WhileCmd : ICommand
    {
        public void Execute(List<string> paramVals, InterpreterState state)
        {
            var endLoopAddress = FindEndLoopAddress(state);
            if (paramVals[0].ToInt() == 1)
            {
                state.LoopAddressStack.Push(state.Cursor);
                return;
            }

            state.Cursor = endLoopAddress;
        }
        
        private int FindEndLoopAddress(InterpreterState state)
        {
            for (int i = state.Cursor; i < state.Program.Count; i++)
            {
                if (state.Program[i].ToLower().Trim() == "endloop")
                {
                    return i;
                }
            }
            throw new ApplicationException($"EndLoop command not found for while loop");
        }
    }
    
    
}