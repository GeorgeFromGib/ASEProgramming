using System.Collections.Generic;
using ASEProgrammingLanguageEnvironment.Commands;
using ASEProgrammingLanguageEnvironment.Utils;
using ASEProgrammingLanguageEnvironment.Utils.Drawing;

namespace ASEProgrammingLanguageEnvironment.Interpreter
{
    public interface ICommandFactory
    {
        Dictionary<string,Lex> cmdList { get; }
    }

    public  class CommandFactory : ICommandFactory
    {
        public  Dictionary<string,Lex> cmdList { get; internal set; }
        public  CommandFactory(Canvass canvass, ProgramListManager progManager)
        {
            cmdList = new Dictionary<string,Lex>
            {
                {"rem",new Lex {CmdRegx = @"rem ([a-zA-Z0-9_ ]*$)", Command = null}},
                {"var", new Lex { CmdRegx = @"var ($VARIABLE$)=($EXPRESSIONORVAL$)", Command = new VarCmd() }}, 
                {"let",new Lex {CmdRegx = @"let ($VARIABLE$)=($EXPRESSIONORVAL$)", Command = new LetCommand()}},
                {"if",new Lex {CmdRegx = @"if ($EXPRESSION$)", Command = new IfCommand()}},
                {"endif",new Lex {CmdRegx = @"endif", Command = null}},
                {"while", new Lex { CmdRegx = @"while ($EXPRESSION$)", Command = new WhileCmd() }},
                {"endloop", new Lex { CmdRegx = @"endloop", Command = new EndLoopCmd() }},
                {"function", new Lex { CmdRegx = @"function ($FUNC$)($FUNCPARAM$)", Command = new FunctionCmd() }},
                {"endfunction", new Lex { CmdRegx = @"endfunction", Command = new EndFunctionCmd() }},
                {"call", new Lex { CmdRegx = @"call ($FUNC$)($FUNCVALUE$)", Command = new CallFunctionCmd() }},
                {"pen",new Lex {CmdRegx = @"pen ($TEXTVALUE$)", Command = new SetPenCmd(canvass)}},
                {"circle",new Lex {CmdRegx = @"circle ($VALUE$)", Command = new DrawCircleCmd(canvass)}},
                {"clear",new Lex {CmdRegx = @"clear", Command = new ClearCmd(canvass)}},
                {"moveto",new Lex {CmdRegx = @"moveto ($VALUE$),($VALUE$)", Command = new PositionPenCmd(canvass)}},
                {"drawto",new Lex {CmdRegx = @"drawto ($VALUE$),($VALUE$)", Command = new DrawToCmd(canvass)}},
                {"fill",new Lex {CmdRegx = @"fill ($TEXTVALUE$)", Command = new FillCmd(canvass)}},
                {"rectangle",new Lex {CmdRegx = @"rectangle ($VALUE$),($VALUE$)", Command = new RectangleCmd(canvass)}},
                {"triangle",new Lex {CmdRegx = @"triangle ($VALUE$),($VALUE$),($VALUE$),($VALUE$),($VALUE$),($VALUE$)", Command = new PolygonCmd(canvass)}},
                {"reset",new Lex {CmdRegx = @"reset", Command = new ResetCmd(canvass)}},
            };
        }

      
    }
}
