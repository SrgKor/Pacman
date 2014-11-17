using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pacman
{
    class PinkGhostImg
    {
        Image up = Properties.Resources.pinkGhostUp;
        Image down = Properties.Resources.pinkGhostDown;
        Image left = Properties.Resources.pinkGhostLeft;
        Image right = Properties.Resources.pinkGhostRight;

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
