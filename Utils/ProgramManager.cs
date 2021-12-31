using System.Collections.Generic;

namespace ASEProgrammingLanguageEnvironment.Utils
{
    public class ProgramListManager
    {
        private readonly ProgramContainer _container;

        public ProgramListManager(ProgramContainer container)
        {
            _container = container;
        }

        public List<string> Program
        {
            get
            {
                return _container.GetProgramList();
            }
            set
            {
                _container.SetProgram(value);
            }
       
        }

        public void LoadProgram()
        {
            
        }
        
        public void SaveProgram()
        {
            // Save program to file
        }
        
    }
}