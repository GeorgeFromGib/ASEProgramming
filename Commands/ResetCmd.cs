using System.Collections.Generic;
using System.Text.RegularExpressions;
using ASEProgrammingLanguageEnvironment.Interpreter;
using ASEProgrammingLanguageEnvironment.Utils;
using ASEProgrammingLanguageEnvironment.Utils.Drawing;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class ResetCmd : GraphicsCommandBase
    {
        public ResetCmd(Canvass canvass) : base(canvass)
        {
        }
        

        public override void Execute(List<string> paramVals, InterpreterState state)
        {
            _canvass.ResetPos();
        }
    }
}