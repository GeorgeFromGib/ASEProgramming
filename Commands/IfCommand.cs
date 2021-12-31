using System;
using System.Collections.Generic;
using ASEProgrammingLanguageEnvironment.Extensions;
using ASEProgrammingLanguageEnvironment.Interpreter;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class IfCommand : ICommand
    {
        public void Execute(List<string> paramVals, InterpreterState state)
        {
            var endIfAddress = FindEndIfAddress(state);
            
            if (paramVals[0].ToInt() == 1) return;

            state.Cursor = endIfAddress;
        }

        private int FindEndIfAddress(InterpreterState state)
        {
            for (int i = state.Cursor; i < state.Program.Count; i++)
            {
                if (state.Program[i].ToLower().Trim() == "endif")
                {
                    return i;
                }
            }
            throw new ApplicationException($"EndIf command not found for If condition");
        }
    }
}