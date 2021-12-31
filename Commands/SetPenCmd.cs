﻿using System.Collections.Generic;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using ASEProgrammingLanguageEnvironment.Interpreter;
using ASEProgrammingLanguageEnvironment.Utils;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class SetPenCmd : GraphicsCommandBase
    {

        public SetPenCmd(Canvass canvass) : base(canvass)
        {
        }


        public override void Execute(List<string> paramVals, ProgramInterpreter.State state)
        {
            var col = paramVals[0].Replace("'", "");
            _canvass.SetPen(col);
        }
    }
}