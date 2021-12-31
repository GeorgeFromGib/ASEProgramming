using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ASEProgrammingLanguageEnvironment.Exceptions;

namespace ASEProgrammingLanguageEnvironment.Interpreter
{
    public partial class ProgramInterpreter
    {
        private readonly ICommandFactory _commandFactory;
        private  InterpreterState _state;

       

        public ProgramInterpreter(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
            _state = new InterpreterState();
        }

        public void Reset()
        {
            _state = new InterpreterState();
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
}