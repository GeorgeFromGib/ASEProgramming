using ASEProgrammingLanguageEnvironment.Commands;

namespace ASEProgrammingLanguageEnvironment.Interpreter
{
    public class Lex
    {
        public string CmdRegx { get; set; }
        public ICommand Command { get; set; }
    }
}