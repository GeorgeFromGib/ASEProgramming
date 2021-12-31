using System.Collections.Generic;
using ASEProgrammingLanguageEnvironment.Commands;
using ASEProgrammingLanguageEnvironment.Interpreter;

namespace ASETestProject.MocksAndSpys
{
    public class SpyCommand : ICommand
    {
        private readonly ICommand _command;
        public List<string> ParameterValues;
        
        public ProgramInterpreter.State State;

        public SpyCommand(ICommand command)
        {
            _command = command;
        }
        public void Execute(List<string> paramVals, ProgramInterpreter.State state)
        {
            ParameterValues = paramVals;
            _command.Execute(paramVals,state);
            State = state;
        }
    }
}