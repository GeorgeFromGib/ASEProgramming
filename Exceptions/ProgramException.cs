using System;

namespace ASEProgrammingLanguageEnvironment.Exceptions
{
    public class ProgramException : ApplicationException
    {
        public int LineNo { get; }
        
        public ProgramException(int lineNo, string message) : base(message)
        {
            LineNo = lineNo;
        }
    }
}