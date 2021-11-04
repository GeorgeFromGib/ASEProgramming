using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProgrammingLanguageEnvironment
{
    //Canvass class holds info that is displayed on the form in response to simple programming language (SPL) commands
    class Canvass
    {
        //instance data for pen and x,y position and graphics context
        //graphics context is the drawing area to draw on (week 3)
        Graphics g;
        Pen Pen;
        int xPos, yPos;  //pen position when drawing
        //constructor initialises canvas to white pen at 0,0
        /// <param name="g">Graphics context of place to draw on</param>
        public Canvass(Graphics g)
        {
            this.g = g;
            xPos = yPos = 0;
            Pen = new Pen(Color.Black, 1); //standard pen (should use constant)
        }
        // draw a line from current pen pos (xPos, yPos)
        /// <param name="toX">x position to draw to</param>
        /// <param name="toY">y position to draw to</param>
        public void DrawLine(int toX, int toY)
        {
            g.DrawLine(Pen, xPos, yPos, toX, toY); //draw the line
            xPos = toX;
            yPos = toY; //update the pen pos as its moved to the end of line 
        }
        public void DrawSquare(int width)
        {
            g.DrawRectangle(Pen, xPos, yPos, xPos + width, yPos + width);
        }
    }
}
