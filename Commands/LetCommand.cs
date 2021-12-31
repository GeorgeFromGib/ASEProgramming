using System.Collections.Generic;
using System.Linq;
using ASEProgrammingLanguageEnvironment.Interpreter;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class LetCommand:ICommand
    {
        public void Execute(List<string> paramVals,ProgramInterpreter.State state)
        {
            var varName = paramVals[0];
            var varValue = paramVals[1];
           
            state.SetVariable(varName,varValue);
        }
        
    }
}