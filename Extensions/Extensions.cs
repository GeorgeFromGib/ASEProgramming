using System;

namespace ASEProgrammingLanguageEnvironment.Extensions
{
    public static class Extensions
    {
        public static int ToInt(this string value)
        {
            return Int32.Parse(value);
        }
    }
}