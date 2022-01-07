using System.Collections.Generic;
using System.Text.RegularExpressions;
using ASEProgrammingLanguageEnvironment.Interpreter;
using ASEProgrammingLanguageEnvironment.Utils;
using ASEProgrammingLanguageEnvironment.Utils.Drawing;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class FillCmd : GraphicsCommandBase
    {


        public FillCmd(Canvass canvass) : base(canvass)
        {
        }

        public override void Execute(List<string> paramVals, InterpreterState state)
        {
            _canvass.SetFill(paramVals[0]=="'on'");
        }
    }
}