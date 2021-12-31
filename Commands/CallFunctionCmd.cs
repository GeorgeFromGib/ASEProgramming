using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ASEProgrammingLanguageEnvironment.Exceptions;
using ASEProgrammingLanguageEnvironment.Interpreter;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class CallFunctionCmd : ICommand
    {
        public void Execute(List<string> paramVals, ProgramInterpreter.State state)
        {
            var funcName = paramVals[0];
            if (state.FuncStore.ContainsKey(funcName))
            {
                state.Scope.Push(funcName);
                var func = state.FuncStore[funcName];
                if (paramVals[1].Length > 2)
                {
                    var values = GetValues(paramVals[1],state);
                    for (int i = 0; i < func.Parameters.Count; i++)
                    {
                        state.AddVariable(func.Parameters[i],values[i]);
                    }
                }
                state.FuncReturnAddressStack.Push(state.Cursor);
                state.Cursor = func.Address;
            }
            else
            {
                throw new FunctionNotFoundException($"Function '{funcName}' is not defined");
            }
        }

        private List<string> GetValues(string funcParamsStr, ProgramInterpreter.State state)
        {
            var values = new List<string>();
            var varReg = Parser.DeTokenise("$VARIABLE$$");
            var str = funcParamsStr.Replace("(", "").Replace(")", "");
            var funcParamVals = str == "" ? new List<string>() : str.Split(',').ToList();
            foreach (var val in funcParamVals)
            {
                values.Add(Regex.IsMatch(val, varReg) ? state.GetVariable(val) : val);
            }
            return values;
        }
    }
}