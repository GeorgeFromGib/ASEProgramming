using System.Collections.Generic;
using ASEProgrammingLanguageEnvironment.Interpreter;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class VarCmd:ICommand
    {
        public void Execute(List<string> paramVals, ProgramInterpreter.State state)
        {
            var varName = paramVals[0];
            var varValue = paramVals[1];
           
            state.AddVariable(varName,varValue);
        }
    }
}