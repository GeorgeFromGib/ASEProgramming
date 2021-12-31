using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using ASEProgrammingLanguageEnvironment.Extensions;
using ASEProgrammingLanguageEnvironment.Interpreter;
using ASEProgrammingLanguageEnvironment.Utils;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class RectangleCmd : GraphicsCommandBase
    {

        public RectangleCmd(Canvass canvass) : base(canvass)
        {
        }
        
        public override void Execute(List<string> paramVals, InterpreterState state)
        {
            _canvass.DrawRectangle(paramVals[0].ToInt(), paramVals[1].ToInt());
        }
    }
}

    