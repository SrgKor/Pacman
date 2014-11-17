using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pacman
{
    // Класс содержит массив изображений - кадров анимации большой точки.
    class BigDotImg
    {
        Image[] bigDotShots = new Image[]
        {
            Properties.Resources.BigDot0,
            Properties.Resources.BigDot1,
            Properties.Resources.BigDot2,
            Properties.Resources.BigDot3,
            Properties.Resources.BigDot4,
            Properties.Resources.BigDot5
        };
        public Image[] BigDotShots
        {
            get { return bigDotShots; }
        }
    }
}
