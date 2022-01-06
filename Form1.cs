using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ASEProgrammingLanguageEnvironment.Exceptions;
using ASEProgrammingLanguageEnvironment.Interpreter;
using ASEProgrammingLanguageEnvironment.Utils;

namespace ASEProgrammingLanguageEnvironment
{
    public partial class Form1 : Form
    {
        private readonly ASEApplication _app;
        private readonly ProgramContainer _progContainer;
        private readonly Bitmap _outputBitmap = new Bitmap(640, 480);


        public Form1()
        {
            InitializeComponent();
            _progContainer = new ProgramContainer(ProgramWindow);
            var canvass = new Canvass(Graphics.FromImage(_outputBitmap));
            canvass.DrawingUpdated += DrawingUpdated;
            _app = new ASEApplication(canvass, _progContainer);
        }

        private void DrawingUpdated(object sender, EventArgs e)
        {
            Refresh();
        }

        private void commandLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CommandEntered(commandLine.Text.Trim().ToLower());
            }
        }

        private void btn_command_Click(object sender, EventArgs e)
        {
            CommandEntered(commandLine.Text.Trim().ToLower());
        }

        private void CommandEntered(string command)
        {
            if (string.IsNullOrEmpty(command))
                return;
            try
            {
                _progContainer.ResetColors();
                _app.ParseCommand(command);
            }
            catch (ProgramException ex)
            {
                _progContainer.HighlightLine(ex.LineNo,Color.Red);
                MessageBox.Show(ex.Message, @"Command error");
            }

            commandLine.Text = ""; //clear the command line
            Refresh(); //signify that the display needs updating
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics; //get graphics context of form which is being displayed
            g.DrawImageUnscaled(_outputBitmap, 0, 0); //put the off screen bitmap on the form
        }

        private void btn_Execute_Click(object sender, EventArgs e)
        {
            CommandEntered("run");
        }

        private void btn_SyntaxHighlight_Click(object sender, EventArgs e)
        {
            _progContainer.ResetColors();
        }
    }
}