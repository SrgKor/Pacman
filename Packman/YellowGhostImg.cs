using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pacman
{
    class YellowGhostImg
    {
        Image up = Properties.Resources.yellowGhostUp;
        Image down = Properties.Resources.yellowGhostDown;
        Image right = Properties.Resources.yellowGhostRight;
        Image left = Properties.Resources.yellowGhostLeft;

        public Image Up
        {
            get { return up; }
        }
        public Image Down
        {
            get { return down; }
        }
        public Image Right
        {
            get { return right; }
        }
        public Image Left
        {
            get { return left; }
        }
    }
}
