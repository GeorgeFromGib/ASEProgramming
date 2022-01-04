using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ASEProgrammingLanguageEnvironment.Utils
{
    public class ProgramContainer
    {
        private readonly RichTextBox _progContainer;
        private readonly Dictionary<string, Color> _cmdCols= new Dictionary<string, Color>
        {
            { "rem", Color.Green },
            { "var", Color.DarkViolet },
            { "let", Color.DodgerBlue },
            { "function", Color.DarkOrange },
            { "endfunction", Color.DarkOrange },
            { "call", Color.DarkOrange },
            { "while", Color.DarkMagenta },
            { "endloop", Color.DarkMagenta },
            { "if", Color.DarkBlue },
            { "endif", Color.DarkBlue },
        };

        public ProgramContainer(RichTextBox programContainer)
        {
            _progContainer = programContainer;
        }

        private void ColorCodeCommands()
        {
            var program = _progContainer.Lines;
            for (var i = 0; i < program.Length; i++)
            {
                var text = program[i].TrimStart().ToLower();
                foreach (var keyVal in _cmdCols)
                {
                    if (!string.IsNullOrEmpty(text) && (text.StartsWith(keyVal.Key) || text == keyVal.Key))
                    {
                        HighlightLine(i, keyVal.Value);
                        break;
                    }
                }
            }
        }

        public List<string> GetProgramList()
        {
            var progList=_progContainer.Text.Split('\n');
            return progList.ToList();
        }
        
        public void SetProgram(List<string> value)
        {
            _progContainer.Text = string.Join("\n",value.ToString());
        }
        
        public void ResetColors()
        {
            int pos = _progContainer.SelectionStart;
            SetSelectionColor(Color.Black, pos,0,_progContainer.TextLength);
            ColorCodeCommands();
        }
        
        public void HighlightLine(int lineNo,Color color)
        {
            var text = _progContainer.Lines[lineNo];
            _progContainer.Select(_progContainer.GetFirstCharIndexFromLine(lineNo), text.Length);
            _progContainer.SelectionColor = color;
        }
        
        private void SetSelectionColor(Color color1,int curPos, int start, int length)
        {
            _progContainer.SelectionStart = start;
            _progContainer.SelectionLength = length;
            _progContainer.SelectionColor = color1;
            _progContainer.SelectionStart = curPos;
            _progContainer.SelectionLength = 0;
        }
    }
}