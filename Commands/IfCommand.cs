using System.Collections.Generic;
using ASEProgrammingLanguageEnvironment.Extensions;
using ASEProgrammingLanguageEnvironment.Interpreter;

namespace ASEProgrammingLanguageEnvironment.Commands
{
    public class IfCommand : ICommand
    {
        public void Execute(List<string> paramVals, ProgramInterpreter.State state)
        {
            if (paramVals[0].ToInt() == 1) return;
            
            for (int i = state.Cursor; i < state.Program.Count; i++)
            {
                if (state.Program[i].ToLower().Trim() == "endif")
                {
                    state.Cursor = i;
                    break;
                }
            }
        }
    }

    public class WhileCmd : ICommand
    {
        public void Execute(List<string> paramVals, ProgramInterpreter.State state)
        {
            if (paramVals[0].ToInt() == 1)
            {
                state.LoopAddressStack.Push(state.Cursor);
                return;
            }
            for (int i = state.Cursor; i < state.Program.Count; i++)
            {
                if (state.Program[i].ToLower().Trim() == "endloop")
                {
                    state.Cursor = i;
                    break;
                }
            }
        }
    }

    public class EndLoopCmd : ICommand
    {
        public void Execute(List<string> paramVals, ProgramInterpreter.State state)
        {
            state.Cursor = state.LoopAddressStack.Pop()-1;
        }
    }
}