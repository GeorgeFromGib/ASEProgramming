﻿using System.Collections.Generic;
using ASEProgrammingLanguageEnvironment.Extensions;
using ASEProgrammingLanguageEnvironment.Interpreter;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class IfCommand : ICommand
    {
        public void Execute(List<string> paramVals, InterpreterState state)
        {
            if (paramVals[0].ToInt() == 1) return;
            
            for (int i = state.Cursor; i < state.Program.Count; i++)
            {
                if (state.Program[i].ToLower().Trim() == "endif")
                {
                    state.Cursor = i;
                    break;
                }
            }
        }
    }
}