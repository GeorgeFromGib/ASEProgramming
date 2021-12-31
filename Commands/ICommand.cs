using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;
using ASEProgrammingLanguageEnvironment.Interpreter;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public interface ICommand
    {
        void Execute(List<string> paramVals, ProgramInterpreter.State state);
    }
}