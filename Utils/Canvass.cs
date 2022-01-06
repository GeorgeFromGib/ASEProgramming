using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace ASEProgrammingLanguageEnvironment.Utils
{
    //Canvass class holds info that is displayed on the form in response to simple programming language (SPL) commands
    public class Canvass
    {
        //instance data for pen and x,y position and graphics context
        //graphics context is the drawing area to draw on (week 3)
        readonly Graphics _g;
        private Pen _pen;
        private int _xPos, _yPos; //pen position when drawing
        private SolidBrush _brush;
        private bool _fill = false;
        private Timer _timer;
        private List<Action> _drawActions;
        private string _penColStr;
        public event EventHandler DrawingUpdated;

        //constructor initialises canvas to white pen at 0,0
        /// <param name="g">Graphics context of place to draw on</param>
        public Canvass(Graphics g)
        {
            _g = g;
            Reset();
            _timer = new Timer(ThreadRunner, null, 0, 500);
        }

        private void ThreadRunner(object state)
        {
            if(_drawActions.Count==0)
                return;
            
            ClearDrawingArea();
            foreach (var drawAction in _drawActions)
            {
                drawAction.Invoke();
            }

            OnDrawingUpdated();
        }

        public void SetPenColor(string color)
        {
            _penColStr = color;
            var col = new ASEColor(color);
        }

        private (Pen pen, Brush brush) GetPenAndBrush(Color col)
        {
            return (new Pen(col, 1), new SolidBrush(col));
        }

        public void SetFill(bool fill)
        {
            _drawActions.Add(() =>
            {
                _fill = fill;
            });
            
        }

        // draw a line from current pen pos (xPos, yPos)
        /// <param name="toX">x position to draw to</param>
        /// <param name="toY">y position to draw to</param>
        public void DrawLine(int toX, int toY)
        {
            var col = new ASEColor(_penColStr);
            _drawActions.Add(() =>
            {
                var (pen,_) = GetPenAndBrush(col.Color);
                _g.DrawLine(pen, _xPos, _yPos, toX, toY); //draw the line
                _xPos = toX;
                _yPos = toY; //update the pen pos as its moved to the end of line 
            });
        }

        public void DrawRectangle(int width, int height)
        {
            var col = new ASEColor(_penColStr);
            _drawActions.Add(() =>
            {
                var (pen,brush) = GetPenAndBrush(col.Color);
                var rect = new Rectangle(_xPos, _yPos, width, height);
                if (_fill)
                    _g.FillRectangle(brush, rect);
                else
                    _g.DrawRectangle(pen, rect);
            });
        }

        public void DrawCircle(int radius)
        {
            var col = new ASEColor(_penColStr);
            _drawActions.Add(() =>
            {
                var (pen,brush) = GetPenAndBrush(col.Color);
                var rect = new Rectangle(_xPos, _yPos, radius, radius);
                if (_fill)
                    _g.FillEllipse(brush, rect);
                else
                    _g.DrawEllipse(pen, rect);
            });
        }

        public void DrawPolygon(Point[] points)
        {
            var col = new ASEColor(_penColStr);
            _drawActions.Add(() =>
            {
                var (pen,brush) = GetPenAndBrush(col.Color);
                if (_fill)
                    _g.FillPolygon(brush, points);
                else
                    _g.DrawPolygon(pen, points);
            });
        }

        public void PositionPen(int xPos, int yPos)
        {
            _drawActions.Add(() =>
            {
                  _xPos = xPos;
                            _yPos = yPos;
            });
          
        }


        public void ClearDrawingArea()
        {
            _g.Clear(Color.Transparent);
        }

        public void ResetPos()
        {
            _drawActions.Add(() =>
            {
                  _xPos = _yPos = 0;
            });
        }

        public void Reset()
        {
            _xPos = _yPos = 0;
            _pen = new Pen(Color.Black, 1); //standard pen (should use constant)
            _brush = new SolidBrush(Color.Black);
            _drawActions = new List<Action>();
        }

        protected virtual void OnDrawingUpdated()
        {
            DrawingUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}