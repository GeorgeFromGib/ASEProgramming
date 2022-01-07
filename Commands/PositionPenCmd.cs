using System.Collections.Generic;
using System.Text.RegularExpressions;
using ASEProgrammingLanguageEnvironment.Extensions;
using ASEProgrammingLanguageEnvironment.Interpreter;
using ASEProgrammingLanguageEnvironment.Utils;
using ASEProgrammingLanguageEnvironment.Utils.Drawing;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class PositionPenCmd:GraphicsCommandBase
    {

        public PositionPenCmd(Canvass canvass) : base(canvass)
        {
        }
        
        public override void Execute(List<string> paramVals, InterpreterState state)
        {
            _canvass.PositionPen(paramVals[0].ToInt(), paramVals[1].ToInt());
        }
    }
}