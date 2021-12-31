using System.Collections.Generic;
using ASEProgrammingLanguageEnvironment.Extensions;
using ASEProgrammingLanguageEnvironment.Interpreter;
using ASEProgrammingLanguageEnvironment.Utils;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class DrawCircleCmd : GraphicsCommandBase
    {
        public DrawCircleCmd(Canvass canvass):base(canvass)
        {
        }

        public override void Execute(List<string> paramVals, ProgramInterpreter.State state)
        {
            _canvass.DrawCircle(paramVals[0].ToInt());
        }
    }

   
}