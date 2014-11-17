using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pacman
{
    class RedGhostImg
    {
        Image up =    Properties.Resources.redGhostUp;
        Image down =  Properties.Resources.redGhostDown;
        Image left =  Properties.Resources.redGhostLeft;
        Image right = Properties.Resources.redGhostRight;

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
