using System.Collections.Generic;
using ASEProgrammingLanguageEnvironment.Commands;
using ASEProgrammingLanguageEnvironment.Exceptions;
using ASEProgrammingLanguageEnvironment.Interpreter;
using ASETestProject.MocksAndSpys;
using NUnit.Framework;

namespace ASETestProject
{
    public class ProgramInterpreterTests
    {
        private SpyLetCommand _cmd;
        private MockCommandFactory _cf;
        private SpyCommand _varSpy;
        private SpyCommand _spy;

        [SetUp]
        public void Setup()
        {
            _cmd = new SpyLetCommand();
            _varSpy = new SpyCommand(new VarCmd());
            _spy = new SpyCommand(null);
            _cf = new MockCommandFactory(
                new Dictionary<string, Lex>
                {
                    {"let", new Lex { CmdRegx = @"let ($VARIABLE$)=($EXPRESSIONORVAL$)", Command = _cmd }}, 
                    {"var", new Lex { CmdRegx = @"var ($VARIABLE$)=($EXPRESSIONORVAL$)", Command = _varSpy }}, 
                    {"if", new Lex { CmdRegx = @"if ($EXPRESSION$)", Command = new IfCommand() }},
                    {"endif", new Lex { CmdRegx = @"endif", Command = null }},
                    {"circle", new Lex { CmdRegx = @"circle ($VALUE$)", Command = null }},
                    {"triangle", new Lex { CmdRegx = @"triangle ($VALUE$),($VALUE$)", Command = _spy }},
                    {"while", new Lex { CmdRegx = @"while ($EXPRESSION$)", Command = new WhileCmd() }},
                    {"endloop", new Lex { CmdRegx = @"endloop", Command = new EndLoopCmd() }},
                    {"function", new Lex { CmdRegx = @"function ($FUNC$)($FUNCPARAM$)", Command = new FunctionCmd() }},
                    {"endfunction", new Lex { CmdRegx = @"endfunction", Command = new EndFunctionCmd() }},
                    {"call", new Lex { CmdRegx = @"call ($FUNC$)($FUNCVALUE$)", Command = new CallFunctionCmd() }},
                });
        }
        

        [Test]
        public void Run_SimpleProgramFlowTest()
        {
            var i = new ProgramInterpreter(_cf);
            var program = new List<string>
            {
                "var B=0",
                "var ab2 = 10+3",
                "let B = ab2 + 10"
            };
            i.Run(program);
            Assert.AreEqual("23", _cmd.State.GetVariable("B"));
        }
        
        [Test]
        public void Run_AssignTextValueToVariable()
        {
            var i = new ProgramInterpreter(_cf);
            var program = new List<string>
            {
                "var col='white'",
            };
            i.Run(program);
            Assert.AreEqual("'white'", _varSpy.State.GetVariable("col"));
        }

        [Test]
        public void Run_CommandWithComaSeparatedValuesTest()
        {
            var i = new ProgramInterpreter(_cf);
            var program = new List<string>
            {
                "triangle 400,200",
            };
            i.Run(program);
            Assert.AreEqual("400", _spy.ParameterValues[0]);
            Assert.AreEqual("200", _spy.ParameterValues[1]);
        }

        [Test]
        public void Run_IfCommandTest()
        {
            var i = new ProgramInterpreter(_cf);
            var program = new List<string>
            {
                "var x = 0",
                "var a = 3",
                "if a == 3",
                "\tlet a=5",
                "\tcircle 10",
                "endif",
                "let x=1"
            };
            i.Run(program);
            Assert.AreEqual("5", _cmd.State.GetVariable("a"));
            Assert.AreEqual("1",_cmd.State.GetVariable("x"));
        }

        [Test]
        public void Run_InvalidCommandCheck()
        {
            var i = new ProgramInterpreter(_cf);
            var program = new List<string>
            {
                "dummy",
            };
            var ex = Assert.Throws<ProgramException>(() => i.Run(program));
            Assert.That(ex.Message.Contains("dummy"));
        }
        
        [Test]
        public void Run_InvalidCommand2Check()
        {
            var i = new ProgramInterpreter(_cf);
            var program = new List<string>
            {
                "rectangle 400 400",
            };
            var ex = Assert.Throws<ProgramException>(() => i.Run(program));
            Assert.That(ex.Message.Contains("rectangle"));
        }
        
        [Test]
        public void Run_InvalidCommandSyntaxCheck()
        {
            var i = new ProgramInterpreter(_cf);
            var program = new List<string>
            {
                "let 2=1",
            };
            var ex = Assert.Throws<ProgramException>(() => i.Run(program));
            Assert.That(ex.Message.Contains("Invalid command syntax"));
        }
        
        [Test]
        public void Run_InvalidExpressionSyntaxCheck()
        {
            var i = new ProgramInterpreter(_cf);
            var program = new List<string>
            {
                "let a='Hello'+2",
            };
            var ex = Assert.Throws<ProgramException>(() => i.Run(program));
            Assert.That(ex.Message.Contains("Invalid command syntax"));
        }
        
        [Test]
        public void Run_VariableNotDefinedTest()
        {
            var i = new ProgramInterpreter(_cf);
            var program = new List<string>
            {
                "let B=A",
            };
            var ex = Assert.Throws<ProgramException>(() => i.Run(program));
            Assert.That(ex.Message.Contains("Variable 'A' is not defined"));
        }

        [Test]
        public void Run_WhileLoopTest()
        {
            var i = new ProgramInterpreter(_cf);
            var program = new List<string>
            {
                "var a = 0",
                "var x = 0",
                "while a<=3",
                "let a=a+1",
                "endloop",
                "let x=1",
                
            };
            i.Run(program);
            Assert.AreEqual("4", _cmd.State.GetVariable("a"));
            Assert.AreEqual("1",_cmd.State.GetVariable("x"));
        }
        
         
        [Test]
        public void Run_FunctionTest()
        {
            var i = new ProgramInterpreter(_cf);
            var program = new List<string>
            {
                "var a=0",
                "var x = 0",
                "function window()",
                "let a=a+1",
                "endfunction",
                "let x=1",
                "call window()",
                
            };
            i.Run(program);
            Assert.AreEqual("1",_cmd.State.GetVariable("x"));
            Assert.AreEqual("1",_cmd.State.GetVariable("a"));
        }
        
        [Test]
        public void Run_FunctionNotFoundTest()
        {
            var i = new ProgramInterpreter(_cf);
            var program = new List<string>
            {
                "call window()",
            };
            var ex = Assert.Throws<ProgramException>(() => i.Run(program));
            Assert.That(ex.Message.Contains("Function 'window' is not defined"));
        
        }
        
        [Test]
        public void Run_DuplicateFunctionDefinedTest()
        {
            var i = new ProgramInterpreter(_cf);
            var program = new List<string>
            {
                "function window(x,y,z)",
                "let a=a+1",
                "endfunction",
                "function window(x,y,z)",
                "let a=a+1",
                "endfunction",
            };
            var ex = Assert.Throws<ProgramException>(() => i.Run(program));
            Assert.That(ex.Message.Contains("Function 'window' has previously been defined"));
        
        }
        
        [Test]
        public void Run_FunctionWithParametersTest()
        {
            var i = new ProgramInterpreter(_cf);
            var program = new List<string>
            {
                "var a=0",
                "var x = 0",
                "function window(x,y,z)",
                "let a=a+1",
                "endfunction",
                "let x=1",
                "call window(x,a,'hello')",
                
            };
            i.Run(program);
            Assert.AreEqual("1",_cmd.State.GetVariable("x"));
            Assert.AreEqual("1",_cmd.State.GetVariable("a"));
        }
    }
    
   

}