using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASEProgrammingLanguageEnvironment
{
    public partial class Form1 : Form
    {
        //bitmap to draw on which will be displayed in the picture box
        Bitmap OutputBitmap = new Bitmap(640, 480);
        Canvass MyCanvass;
        public Form1()
        {
            InitializeComponent();
            MyCanvass = new Canvass(Graphics.FromImage(OutputBitmap)); //to handle the drawing, pass the drawing surface to it
        }
        private void commandLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                String Command = commandLine.Text.Trim().ToLower();
                if (Command.Equals("line") == true)
                {
                    MyCanvass.DrawLine(160, 120);
                    Console.WriteLine("LINE");
                }
                else if (Command.Equals("square") == true)
                {
                    MyCanvass.DrawSquare(25);
                    Console.WriteLine("LINE");
                }
                commandLine.Text = ""; //clear the command line
                Refresh(); //signify that the display needs updating

            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; //get graphics context of form which is being displayed
            g.DrawImageUnscaled(OutputBitmap, 0, 0); //put the off screen bitmap on the form
        }
    }
}
