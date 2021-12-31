using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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
            var endFunctionAddress = FindEndfunctionAddress(state, funcName);
            var funcData = new FuncData
            {
                Address = state.Cursor, 
                Parameters = AddAnyFunctionParameterDefinitions(paramVals)
            };
            state.FuncStore.Add(funcName, funcData);
            state.Cursor = endFunctionAddress;

        }

        private int FindEndfunctionAddress(InterpreterState state, string funcName)
        {
            for (var i = state.Cursor; i < state.Program.Count; i++)
            {
                if (state.Program[i].ToLower().Trim() == "endfunction")
                {
                    return i;
                }
            }
            throw new ApplicationException($"EndFunction command not found for function '{funcName}'");
        }

        private static List<string> AddAnyFunctionParameterDefinitions(List<string> paramVals)
        {
            var funcParams = RemoveBrackets(paramVals);
            return funcParams==""?new List<string>():funcParams.Split(',').ToList();
        }

        private static string RemoveBrackets(List<string> paramVals)
        {
            return paramVals[1].Replace("(", "").Replace(")", "");
        }
    }
}