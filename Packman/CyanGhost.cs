using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pacman
{
    sealed class CyanGhost : Enemy
    {
        public CyanGhost()
        {
            defaultX = 148;
            defaultY = 180;
            X = defaultX;
            Y = defaultY;
            ManageImage(1, 0);
        }
        
        CyanGhostImg cyanGhostImg = new CyanGhostImg();

        public override void ManageImage(int dirX, int dirY)
        {
                if (dirX == 1)
                    currentImg = cyanGhostImg.Right;
                else if (dirX == -1)
                    currentImg = cyanGhostImg.Left;
                else if (dirY == 1)
                    currentImg = cyanGhostImg.Down;
                else
                    currentImg = cyanGhostImg.Up;
        }
    }
}
