using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    sealed class YellowGhost : Enemy
    {
        public YellowGhost()
        {
            defaultX = 171;
            defaultY = 140;
            X = defaultX;
            Y = defaultY;
            ManageImage(1, 0);
        }

        YellowGhostImg yellowGhostImg = new YellowGhostImg();


        public override void ManageImage(int dirX, int dirY)
        {
                if (dirX == 1)
                    currentImg = yellowGhostImg.Right;
                else if (dirX == -1)
                    currentImg = yellowGhostImg.Left;
                else if (dirY == 1)
                    currentImg = yellowGhostImg.Down;
                else
                    currentImg = yellowGhostImg.Up;
        }
    }
}
