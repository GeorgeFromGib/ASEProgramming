using System;
using System.Collections.Generic;
using System.Linq;
using ASEProgrammingLanguageEnvironment.Interpreter;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class FunctionCmd : ICommand
    {
        public void Execute(List<string> paramVals, InterpreterState state)
        {
            var funcName = paramVals[0];
            if (state.FuncStore.ContainsKey(funcName))
                throw new ApplicationException($"Function '{funcName}' has previously been defined");
            
            var funcParams=paramVals[1].Replace("(", "").Replace(")", "");
            var funcData = new FuncData
            {
                Address = state.Cursor, 
                Parameters = funcParams==""?new List<string>():funcParams.Split(',').ToList()
            };
            state.FuncStore.Add(funcName, funcData);
            for (var i = state.Cursor; i < state.Program.Count; i++)
            {
                if (state.Program[i].ToLower().Trim() == "endfunction")
                {
                    state.Cursor = i;
                    break;
                }
            }
        }
    }
}