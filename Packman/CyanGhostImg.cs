using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pacman
{
    class CyanGhostImg
    {
        Image up = Properties.Resources.cyanGhostUp;
        Image down = Properties.Resources.cyanGhostDown;
        Image left = Properties.Resources.cyanGhostLeft;
        Image right = Properties.Resources.cyanGhostRight;

        public Image Up
        {
            get { return up; }
        }
        public Image Down
        {
            get { return down; }
        }
        public Image Left
        {
            get { return left; }
        }
        public Image Right
        {
            get { return right; }
        }
    }
}
