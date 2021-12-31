﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ASEProgrammingLanguageEnvironment.Exceptions;

namespace ASEProgrammingLanguageEnvironment.Interpreter
{
    public class ProgramInterpreter
    {
        private readonly ICommandFactory _commandFactory;
        private  State _state;

        public class FuncData
        {
            public int Address { get; set; }
            public List<string> Parameters { get; set; }
        }
        
        public class State
        {
            private readonly Dictionary<string, string> _variables = new Dictionary<string, string>();
            public int Cursor;
            public List<string> Program = new List<string>();
            public Stack<int> LoopAddressStack = new Stack<int>();
            public Dictionary<string, FuncData> FuncStore=new Dictionary<string, FuncData>();
            public Stack<int> FuncReturnAddressStack = new Stack<int>();
            public Stack<string> Scope = new Stack<string>();

            public void SetVariable(string name,string value)
            {
                if (!FindStoredName(name, out var storedName))
                    throw new VariableNotFoundException($"Variable '{name}' is not defined");
                
                _variables[storedName] = value;
            }

            public void AddVariable(string name, string value)
            {
                var scopeName = ScopeVarName(Scope.Peek(), name);
                if (_variables.ContainsKey(scopeName))
                    _variables.Remove(scopeName);
                _variables.Add(scopeName,value);
            }
            
            public string GetVariable(string name)
            {
                if (FindStoredName(name, out var s)) 
                    return _variables[s];

                throw new VariableNotFoundException($"Variable '{name}' is not defined");
            }

            private bool FindStoredName(string name, out string storedName)
            {
                foreach (var item in Scope)
                {
                    var scopeName = ScopeVarName(item, name);
                    if (_variables.ContainsKey(scopeName))
                    {
                        {
                            storedName = scopeName;
                            return true;
                        }
                    }
                }
                storedName = null;
                return false;
            }

            private string ScopeVarName(string scopeName,string name)
            {
                return $"$$${scopeName}_{name}";
            }

        }

        public ProgramInterpreter(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
            _state = new State();
        }

        public void Reset()
        {
            _state = new State();
        }

        public void Run(List<string> program)
        {
            _state.Cursor = 0;
            _state.Scope.Clear();
            _state.Scope.Push("global");
            _state.Program = program;
            ProgramSyntaxCheck(program);
            try
            {
                while (_state.Cursor < program.Count)
                {
                    var commandStr = program[_state.Cursor];
                    Parse(commandStr);
                    _state.Cursor++;
                }
            }
            catch (ApplicationException ex)
            {
                throw new ProgramException(_state.Cursor, ex.Message);
            }
        }

        public void ProgramSyntaxCheck(List<string> program)
        {
            for (int i = 0; i < program.Count; i++)
            {
                var commandStr = program[i];
                if (string.IsNullOrEmpty(commandStr))
                    continue;
                try
                {
                    var cmdLex = GetCommandFromFactory(commandStr);
                    var match = Parser.GetParamGroups(commandStr, cmdLex.CmdRegx);
                    if (!match.Success)
                        throw new ProgramException(i, $"Invalid command syntax '{commandStr}'");
                }
                catch (KeyNotFoundException)
                {
                    throw new ProgramException(i, $"Invalid command '{Parser.ExtractKeyword(commandStr)}'");
                }
            }
        }

        public void Parse(string command)
        {
            if (string.IsNullOrEmpty(command))
                return;
            var cmdLex = GetCommandFromFactory(command);
            var paramVals = Parser.EvaluateCommand(command, cmdLex.CmdRegx, _state);
            cmdLex.Command?.Execute(paramVals, _state);
        }

        private Lex GetCommandFromFactory(string command)
        {
            var keyword = Parser.ExtractKeyword(command);
            var cmdLex = _commandFactory.cmdList[keyword];
            return cmdLex;
        }
    }

    public static class Parser
    {
        private static readonly Dictionary<string, string> _parserTokens = new Dictionary<string, string>
        {
            { "$NUMBER$", @"\d+\.?\d*" },
            { "$WORD$", @"\w+" },
            { "$TEXT$",@"'[a-zA-Z0-9_ ]*'"},
            { "$VARIABLE$", @"[a-zA-Z]+\d*" },
            { "$FUNCPARAM$", @"\(($VARIABLE$|$VARIABLE$(,$VARIABLE$)*)*\)" },
            { "$FUNCVALUE$", @"\((($VALUE$)|($VALUE$)(,($VALUE$))*)*\)" },
            { "$FUNC$", @"$VARIABLE$" },
            { "$OPERAND$", @"[\+\-\/\*]|==|!=|>=|<=|[<>]" },
            { "$VALUE$", "$NUMBER$|$VARIABLE$|$TEXT$" },
            { "$NUMVALUE$", "$NUMBER$|$VARIABLE$" },
            { "$TEXTVALUE$", "$TEXT$|$VARIABLE$" },
            { "$EXPRESSION$", @"($NUMVALUE$)$|($NUMVALUE$)(($OPERAND$)($NUMVALUE$))+" },
            { "$EXPRESSIONORVAL$", @"($EXPRESSION$)|($VALUE$)$" }
        };

        private static Dictionary<string, string> _cache = new Dictionary<string, string>();
        private const string TokensRgx = @"\$\w+\$";

        public static void ClearCache()
        {
            _cache = new Dictionary<string, string>();
        }

        public static List<string> EvaluateCommand(string command, string tokenisedCmd,
            ProgramInterpreter.State state)
        {
            var expPos = Regex.Matches(tokenisedCmd, TokensRgx);
            var match = GetParamGroups(command, tokenisedCmd);
            var paramVals = new List<string>();
            for (var i = 0; i < expPos.Count; i++)
            {
                paramVals.Add(new[]{"$EXPRESSION$","$VALUE$","$EXPRESSIONORVAL$,$TEXTVALUE$"}.Any(c => c.Contains(expPos[i].Value))
                    ? ParseExpression(match.Groups[i + 1].Value, state)
                    : match.Groups[i + 1].Value);
            }

            return paramVals;
        }

        public static Match GetParamGroups(string command, string tokenisedCmd)
        {
            var detokRgx = DeTokeniseCommand(tokenisedCmd);
            var normCmd = NormaliseCmdStr(command, ExtractKeyword(command));
            var match = Regex.Match(normCmd, detokRgx);
            return match;
        }

        public static string DeTokeniseCommand(string command)
        {
            var keyword = ExtractKeyword(command);
            if (_cache.ContainsKey(keyword))
                return _cache[keyword];
            var detoken = DeTokenise(command);
            _cache.Add(keyword, detoken);
            return detoken;
        }

        public static string DeTokenise(string expression)
        {
            var deTokenStr = expression;
            var tokensInStr = Regex.Matches(deTokenStr, TokensRgx);

            if (tokensInStr.Count < 1) return deTokenStr;

            foreach (Match match in tokensInStr)
            {
                deTokenStr = deTokenStr.Replace(match.Value, _parserTokens[match.Value]);
            }

            deTokenStr = DeTokenise(deTokenStr);

            return deTokenStr;
        }

        public static string ExtractKeyword(string command)
        {
            var keywordRgx = @"^\s*\w+";
            var matches = Regex.Match(command, keywordRgx);
            return matches.Value.ToLower().Trim();
        }

        public static string NormaliseCmdStr(string command, string keyword)
        {
            return keyword + " " + command.Trim().Substring(keyword.Length).Replace(" ", "");
        }

        public static string ParseExpression(string command, ProgramInterpreter.State state)
        {
            var operands = ExtractOperands(command);
            var values = ExtractValues(command, state);
            return operands.Count>0 ? Evaluate(values, operands) : values[0];
        }

        private static string Evaluate(IReadOnlyList<string> values, IReadOnlyList<string> operands)
        {
            var result = decimal.Parse(values[0]);
            for (var i = 0; i < operands.Count; i++)
            {
                var val2 = decimal.Parse(values[i + 1]);
                switch (operands[i])
                {
                    case "+":
                        result += val2;
                        break;
                    case "-":
                        result -= val2;
                        break;
                    case "*":
                        result *= val2;
                        break;
                    case "/":
                        result /= val2;
                        break;
                    case "==":
                        result = Convert.ToDecimal(result == val2);
                        break;
                    case "!=":
                        result = Convert.ToDecimal(result != val2);
                        break;
                    case ">=":
                        result = Convert.ToDecimal(result >= val2);
                        break;
                    case "<=":
                        result = Convert.ToDecimal(result <= val2);
                        break;
                    case ">":
                        result = Convert.ToDecimal(result > val2);
                        break;
                    case "<":
                        result = Convert.ToDecimal(result < val2);
                        break;
                }
            }

            return result.ToString();
        }

        private static List<string> ExtractValues(string expression, ProgramInterpreter.State state)
        {
            var valuesRgx = DeTokenise("$VALUE$");
            var variablesRgx = DeTokenise("$VARIABLE$");
            var textRgx = DeTokenise("$TEXT$");
            var tokens = ExtractTokens(expression, valuesRgx);
            var values = new List<string>();
            foreach (var token in tokens)
            {
                try
                {
                    if (!Regex.IsMatch(token,textRgx) && Regex.IsMatch(token, variablesRgx))
                        values.Add(state.GetVariable(token));
                    else
                    {
                        values.Add(token);
                    }
                }
                catch (KeyNotFoundException)
                {
                    throw new VariableNotFoundException($"Variable '{token}' is not defined");
                }
            }

            return values;
        }

        private static List<string> ExtractOperands(string expression)
        {
            var operandsRgx = DeTokenise("$OPERAND$");
            return ExtractTokens(expression, operandsRgx);
        }

        private static List<string> ExtractTokens(string expression, string tokensRgx)
        {
            var matches = Regex.Matches(expression, tokensRgx);
            var tokens = (from Match match in matches select match.Value).ToList();
            return tokens;
        }
    }

   
}