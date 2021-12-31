using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using ASEProgrammingLanguageEnvironment.Extensions;
using ASEProgrammingLanguageEnvironment.Interpreter;
using ASEProgrammingLanguageEnvironment.Utils;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class PolygonCmd : GraphicsCommandBase
    {
        public PolygonCmd(Canvass canvass) : base(canvass)
        {
        }
        
        public override void Execute(List<string> paramVals, InterpreterState state)
        {
            var points = new List<Point>();
            var pi = paramVals.Count;
            for (int i = 0; i < pi; i+=2)
            {
                points.Add(new Point(paramVals[i].ToInt(),paramVals[i+1].ToInt()));
            }
            _canvass.DrawPolygon(points.ToArray());
        }
    }
}