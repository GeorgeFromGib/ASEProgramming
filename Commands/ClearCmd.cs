using System.Collections.Generic;
using System.Text.RegularExpressions;
using ASEProgrammingLanguageEnvironment.Interpreter;
using ASEProgrammingLanguageEnvironment.Utils;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class ClearCmd:GraphicsCommandBase
    {
        public ClearCmd(Canvass canvass) : base(canvass)
        {
        }
        
        public override void Execute(List<string> paramVals, ProgramInterpreter.State state)
        {
            _canvass.Clear();
        }
    }
}