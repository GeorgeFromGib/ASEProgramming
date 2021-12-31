using System;

namespace ASEProgrammingLanguageEnvironment.Exceptions
{
    public class VariableNotFoundException : ApplicationException
    {
        public VariableNotFoundException(string message):base(message)
        {
        }
    }
    public class ProgramException : ApplicationException
    {
        public int LineNo { get; }
        
        public ProgramException(int lineNo, string message) : base(message)
        {
            LineNo = lineNo;
        }
    }
}