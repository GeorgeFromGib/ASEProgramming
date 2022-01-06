using System;
using System.Collections.Generic;
using System.Drawing;

namespace ASEProgrammingLanguageEnvironment.Utils
{
    public class ASEColor
    {
        private readonly bool _flashing;
        private readonly string _colorName;
        private int _colSwitch=0;

        private Dictionary<string, Color[]> _flashcolors = new Dictionary<string, Color[]>
        {
            { "redgreen", new[]{Color.Red, Color.Green} },
            { "blueyellow", new[]{Color.Blue, Color.Yellow} },
            { "blackwhite", new[]{Color.Black, Color.White} },
        };
        
        public ASEColor(string color)
        {
            _colorName = color;
            if (_flashcolors.ContainsKey(color))
            {
                _flashing = true;
                return;
            }
            if (Color.FromName(color).ToArgb() == 0)
                throw new ApplicationException($"Unknown color '{color}'");
        }


        public Color Color
        {
            get
            {
                if (!_flashing) 
                    return Color.FromName(_colorName);
                
                _colSwitch = 1-_colSwitch;
                return _flashcolors[_colorName][_colSwitch];
            }
        }
    }
}