using System;
using System.Collections.Generic;
using System.Drawing;

namespace ASEProgrammingLanguageEnvironment.Utils.Drawing
{
    public class ASEColor
    {
        private readonly bool _flashing;
        private readonly string _colorName;
        private int _colSwitch=0;

        private readonly Dictionary<string, Color[]> _flashColors = new Dictionary<string, Color[]>
        {
            { "redgreen", new[]{Color.Red, Color.Green} },
            { "blueyellow", new[]{Color.Blue, Color.Yellow} },
            { "blackwhite", new[]{Color.Black, Color.White} },
        };
        
        public ASEColor(string color)
        {
            _colorName = color;
            if (_flashColors.ContainsKey(color))
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
                return _flashColors[_colorName][_colSwitch];
            }
        }
    }
}