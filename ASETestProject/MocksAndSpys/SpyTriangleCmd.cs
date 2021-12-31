using System.Collections.Generic;
using ASEProgrammingLanguageEnvironment.Commands;
using ASEProgrammingLanguageEnvironment.Interpreter;

namespace ASETestProject.MocksAndSpys
{
    

    public class SpyTriangleCmd : ICommand
    {
        public List<string> parameterValues;
        public void Execute(List<string> paramVals, ProgramInterpreter.State state)
        {
            parameterValues = paramVals;
        }
    }
}