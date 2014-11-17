using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    sealed class RedGhost : Enemy
    {
        public RedGhost()
        {
            defaultX = 195;
            defaultY = 180;
            X = defaultX;
            Y = defaultY;
            ManageImage(1, 0);
        }

        RedGhostImg redGhostImg = new RedGhostImg();


        public override void ManageImage(int dirX, int dirY)
        {
                if (dirX == 1)
                    currentImg = redGhostImg.Right;
                else if (dirX == -1)
                    currentImg = redGhostImg.Left;
                else if (dirY == 1)
                    currentImg = redGhostImg.Down;
                else
                    currentImg = redGhostImg.Up;
        }
    }
}
