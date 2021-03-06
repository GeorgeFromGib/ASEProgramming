using System.Collections.Generic;
using ASEProgrammingLanguageEnvironment.Extensions;
using ASEProgrammingLanguageEnvironment.Interpreter;
using ASEProgrammingLanguageEnvironment.Utils;
using ASEProgrammingLanguageEnvironment.Utils.Drawing;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class DrawCircleCmd : GraphicsCommandBase
    {
        public DrawCircleCmd(Canvass canvass):base(canvass)
        {
        }

        public override void Execute(List<string> paramVals, InterpreterState state)
        {
            _canvass.DrawCircle(paramVals[0].ToInt());
        }
    }

   
}