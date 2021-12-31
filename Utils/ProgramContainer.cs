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
            { "while", Color.DarkMagenta },
            { "endwhile", Color.DarkMagenta },
            { "if", Color.DarkBlue },
            { "endif", Color.DarkBlue },
        };

        public ProgramContainer(RichTextBox programContainer)
        {
            _progContainer = programContainer;

        }

        private void ColorCodeCommands()
        {
            var program = GetProgramList();
            for (var i = 0; i < program.Count; i++)
            {
                var text = program[i].TrimStart().ToLower();
                foreach (var keyVal in _cmdCols)
                {
                    if(text.StartsWith(keyVal.Key) || text==keyVal.Key)
                        HighlightLine(i,keyVal.Value);
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
            SetColorSelection(Color.Black, pos,0,_progContainer.TextLength);
            ColorCodeCommands();
        }
        
        
        public void HighlightLine(int lineNo,Color color)
        {
            var tag = GetProgramList()[lineNo];
            SetLineColor(tag, color);
        }
        
        private void SetLineColor(string tag, Color color1)
        {
            int pos = _progContainer.SelectionStart;
            string s = _progContainer.Text;
            int jx = s.IndexOf(tag, StringComparison.CurrentCultureIgnoreCase);
            if (jx < 0) return;
            SetColorSelection(color1, pos, jx, tag.Length);
        }

        private void SetColorSelection(Color color1,int curPos, int start, int length)
        {
            _progContainer.SelectionStart = start;
            _progContainer.SelectionLength = length;
            _progContainer.SelectionColor = color1;
            // _progContainer.SelectionStart = curPos;
            _progContainer.SelectionLength = 0;
        }
    }
}