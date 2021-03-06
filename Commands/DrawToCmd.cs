using System.Collections.Generic;
using System.Text.RegularExpressions;
using ASEProgrammingLanguageEnvironment.Extensions;
using ASEProgrammingLanguageEnvironment.Interpreter;
using ASEProgrammingLanguageEnvironment.Utils;
using ASEProgrammingLanguageEnvironment.Utils.Drawing;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class DrawToCmd : GraphicsCommandBase
    {
        public DrawToCmd(Canvass canvass) : base(canvass)
        {
        }

        public override void Execute(List<string> paramVals, InterpreterState state)
        {
            _canvass.DrawLine(paramVals[0].ToInt(), paramVals[1].ToInt());
        }
    }
}