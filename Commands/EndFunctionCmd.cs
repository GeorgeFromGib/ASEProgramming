﻿using System.Collections.Generic;
using ASEProgrammingLanguageEnvironment.Interpreter;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class EndFunctionCmd : ICommand
    {
        public void Execute(List<string> paramVals, ProgramInterpreter.State state)
        {
            state.Cursor = state.FuncReturnAddressStack.Pop();
            state.Scope.Pop();
        }
    }
}