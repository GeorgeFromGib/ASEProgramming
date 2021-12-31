using System.Collections.Generic;
using System.Security.Cryptography;
using ASEProgrammingLanguageEnvironment.Interpreter;
using NUnit.Framework;

namespace ASETestProject
{
    public class ExpressionParserTests
    {
        private InterpreterState _state;

        [SetUp]
        public void Setup()
        {
            _state = new InterpreterState();
            _state.Scope.Push("global");
        }
        
        [Test]
        public void Parse_SimpleDeTokeniseTest()
        {
            Parser.ClearCache();
            var res = Parser.DeTokenise("let ($VARIABLE$)(=)($NUMBER$)");
            Assert.AreEqual(@"let ([a-zA-Z]+\d*)(=)(\d+\.?\d*)",res);
        }
        
        [Test]
        public void Parse_RecursiveDeTokeniseTest()
        {
            Parser.ClearCache();
            var res = Parser.DeTokenise("let ($VARIABLE$)(=)($EXPRESSION$)");
            Assert.AreEqual(@"let ([a-zA-Z]+\d*)(=)((\d+\.?\d*|[a-zA-Z]+\d*)$|(\d+\.?\d*|[a-zA-Z]+\d*)(([\+\-\/\*]|==|!=|>=|<=|[<>])(\d+\.?\d*|[a-zA-Z]+\d*))+)",res);
        }
        
        [Test]
        public void Parse_ValuesOnlyMathTest()
        {
            var ops = Parser.ParseExpression("2+30-1*5/2",_state);
            Assert.AreEqual("77.5",ops);
            
        }
        
        [Test]
        public void Parse_ValuesAndVariablesMathTest()
        {
           
            _state.AddVariable("ab","30");
            var ops = Parser.ParseExpression("2+ab*2",_state);
            Assert.AreEqual("64",ops);
            
        }
        
        [Test]
        public void Parse_TextTest()
        {
            _state.AddVariable("a",@"'red'");
            var ops = Parser.ParseExpression("a",_state);
            Assert.AreEqual("'red'",ops);
        }
        
        [Ignore("Not implemented")]
        public void Parse_DetectInvalidExpressionTest()
        {
            _state.AddVariable("a",@"'red'");
            var ops = Parser.ParseExpression("a+2",_state);
            Assert.AreEqual("'red'",ops);
        }

        [Test]
        public void EvaluateCommand_ValuesOnlyTest()
        {
            var cmd = "let ab2=10+3";
            var tok = @"let ($VARIABLE$)=($EXPRESSION$)";
            var res=Parser.EvaluateCommand(cmd, tok,null);
            Assert.AreEqual("13",res[1]);
        }
        
        [Test]
        public void EvaluateCommand_VariablesOnlyTest()
        {
            
            _state.AddVariable("a","100");
            _state.AddVariable("b","3");
            _state.AddVariable("c","5");
            
            var cmd = "let ab2=a+b+c";
            var tok = @"let ($VARIABLE$)=($EXPRESSION$)";
            var res=Parser.EvaluateCommand(cmd, tok,_state);
            Assert.AreEqual("108",res[1]);
        }

        [Test]
        public void Evaluate_BooleanEqualTest()
        {
            _state.AddVariable("a","100");
            _state.AddVariable("b","100");
            var cmd = "if a==b";
            var tok = @"if ($EXPRESSION$)";
            var res = Parser.EvaluateCommand(cmd, tok, _state);
            Assert.AreEqual("1",res[0]);
        }
        
        [Test]
        public void Evaluate_BooleanNotEqualTest()
        {
            
            _state.AddVariable("a","100");
            _state.AddVariable("b","200");
            var cmd = "if a!=b";
            var tok = @"if ($EXPRESSION$)";
            var res = Parser.EvaluateCommand(cmd, tok, _state);
            Assert.AreEqual("1",res[0]);
        }
        
        [Test]
        public void Evaluate_BooleanLessThanTest()
        {
            
            _state.AddVariable("a","100");
            _state.AddVariable("b","200");
            var cmd = "if a<b";
            var tok = @"if ($EXPRESSION$)";
            var res = Parser.EvaluateCommand(cmd, tok, _state);
            Assert.AreEqual("1",res[0]);
        }
        
        [Test]
        public void Evaluate_BooleanMoreThanTest()
        {
           
            _state.AddVariable("a","100");
            _state.AddVariable("b","200");
            var cmd = "if b>a";
            var tok = @"if ($EXPRESSION$)";
            var res = Parser.EvaluateCommand(cmd, tok, _state);
            Assert.AreEqual("1",res[0]);
        }

      
    }
}