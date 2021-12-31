using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ASEProgrammingLanguageEnvironment.Exceptions;
using ASEProgrammingLanguageEnvironment.Interpreter;
using ASEProgrammingLanguageEnvironment.Utils;

namespace ASEProgrammingLanguageEnvironment
{
    public class ASEApplication
    {
        private readonly Canvass _myCanvass;
        private ProgramListManager _progManager;
        private CommandFactory _commandList;
        
        private readonly ProgramInterpreter _interpreter;

        public ASEApplication(Bitmap outputBitmap,ProgramContainer container)
        {
            _myCanvass = new Canvass(
                Graphics.FromImage(outputBitmap)); //to handle the drawing, pass the drawing surface to it
            _progManager = new ProgramListManager(container);
            _commandList = new CommandFactory(_myCanvass, _progManager);
            _interpreter = new ProgramInterpreter(_commandList);
        }
        public void ParseCommand(string command)
        {
            switch (command)
            {
                case "run":
                    _interpreter.Reset();
                    _interpreter.Run(_progManager.Program);
                    break;
                default:
                    _interpreter.Run(new List<string> { command });
                    break;
            }
        }

        public void UpdateProgramList(List<string> programList)
        {
            _progManager.Program = programList;
        }
    }
}