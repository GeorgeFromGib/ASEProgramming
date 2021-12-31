using System;
using System.Collections.Generic;

namespace ASEProgrammingLanguageEnvironment.Interpreter
{
    public class InterpreterState
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
                throw new ApplicationException($"Variable '{name}' is not defined");
                
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

            throw new ApplicationException($"Variable '{name}' is not defined");
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
}