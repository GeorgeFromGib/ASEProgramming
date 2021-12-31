using System.Drawing;

namespace ASEProgrammingLanguageEnvironment.Utils
{
    //Canvass class holds info that is displayed on the form in response to simple programming language (SPL) commands
    public class Canvass
    {
        //instance data for pen and x,y position and graphics context
        //graphics context is the drawing area to draw on (week 3)
        Graphics g;
        Pen _pen;
        int xPos, yPos;  //pen position when drawing
        SolidBrush _brush;
        private bool _fill = false;

        //constructor initialises canvas to white pen at 0,0
        /// <param name="g">Graphics context of place to draw on</param>
        public Canvass(Graphics g)
        {
            this.g = g;
            xPos = yPos = 0;
            _pen = new Pen(Color.Black, 1); //standard pen (should use constant)
            _brush=new SolidBrush(Color.Black);
        }
        // draw a line from current pen pos (xPos, yPos)
        /// <param name="toX">x position to draw to</param>
        /// <param name="toY">y position to draw to</param>
        public void DrawLine(int toX, int toY)
        {
            g.DrawLine(_pen, xPos, yPos, toX, toY); //draw the line
            xPos = toX;
            yPos = toY; //update the pen pos as its moved to the end of line 
        }
        public void DrawRectangle(int width, int height)
        {
            var rect=new Rectangle(xPos, yPos, width, height);
            if(_fill)
                g.FillRectangle(_brush,rect);
            else 
                g.DrawRectangle(_pen,rect); 
        }
        public void DrawCircle(int radius)
        {
            var rect = new Rectangle(xPos, yPos, radius,radius);
            if(_fill)
                g.FillEllipse(_brush,rect);
            else
                g.DrawEllipse(_pen, rect);
        }

        public void DrawPolygon(Point[] points)
        {
            if(_fill)
                g.FillPolygon(_brush,points);
            else
                g.DrawPolygon(_pen,points);
        }

        public void PositionPen(int xPos, int yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;
        }

        public void SetPen(string color)
        {
            var col = Color.FromName(color);
            _pen=new Pen(col,1);
            _brush=new SolidBrush(col);
        }

        
        public void SetFill(bool fill)
        {
            _fill = fill;
        }

        public void Clear()
        {
            g.Clear(Color.Transparent);
        }

        public void Reset()
        {
            xPos = yPos = 0;
        }
    }
}
