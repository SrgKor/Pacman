using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    sealed class PinkGhost : Enemy
    {
        public PinkGhost()
        {
            defaultX = 171;
            defaultY = 180;
            X = defaultX;
            Y = defaultY;
            ManageImage(1, 0);
        }

        PinkGhostImg pinkGhostImg = new PinkGhostImg();

        

        

        public override void ManageImage(int dirX, int dirY)
        {
                if (dirX == 1)
                    currentImg = pinkGhostImg.Right;
                else if (dirX == -1)
                    currentImg = pinkGhostImg.Left;
                else if (dirY == 1)
                    currentImg = pinkGhostImg.Down;
                else
                    currentImg = pinkGhostImg.Up;
        }
    }
}
