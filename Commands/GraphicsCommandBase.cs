using System.Collections.Generic;
using System.Text.RegularExpressions;
using ASEProgrammingLanguageEnvironment.Interpreter;
using ASEProgrammingLanguageEnvironment.Utils;
using ASEProgrammingLanguageEnvironment.Utils.Drawing;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public abstract class GraphicsCommandBase:ICommand
    {
        protected readonly Canvass _canvass;

        protected GraphicsCommandBase(Canvass canvass)
        {
            _canvass = canvass;
        }
        
        public abstract void Execute(List<string> paramVals, InterpreterState state);

    }
}