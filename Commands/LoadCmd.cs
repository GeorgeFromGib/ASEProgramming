using System.Collections.Generic;
using System.Text.RegularExpressions;
using ASEProgrammingLanguageEnvironment.Interpreter;
using ASEProgrammingLanguageEnvironment.Utils;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class LoadCmd : ICommand
    {
        private readonly ProgramListManager _progMan;

        public LoadCmd(ProgramListManager progMan)
        {
            _progMan = progMan;
        }
        public void Execute(GroupCollection @group)
        {
            _progMan.LoadProgram();
        }


        public void Execute(List<string> paramVals, ProgramInterpreter.State state)
        {
            throw new System.NotImplementedException();
        }
    }
}