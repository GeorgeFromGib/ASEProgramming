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
            for (var i = state.Cursor+1; i < state.Program.Count; i++)
            {
                switch (Parser.ExtractKeyword(state.Program[i]))
                {
                    case "if":
                        throw new ApplicationException("If commands can not have other nested If statements");
                    case "endif":
                        return i;
                }
            }
            throw new ApplicationException($"EndIf command not found for If condition");
        }
    }
}