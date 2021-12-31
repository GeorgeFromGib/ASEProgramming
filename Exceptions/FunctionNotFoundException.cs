using System;

namespace ASEProgrammingLanguageEnvironment.Exceptions
{
    public class FunctionNotFoundException : ApplicationException
    {
        public FunctionNotFoundException(string message):base(message)
        {
        }
    }
}