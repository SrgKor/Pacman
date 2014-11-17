using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pacman
{
    // Отображение пакмена.
    class PacmanImg
    {
        Image[] up = new Image[]
        {
            Properties.Resources.packManUp0,
            Properties.Resources.packManUp1,
            Properties.Resources.packManUp2,
            Properties.Resources.packManUp3,
            Properties.Resources.packManUp31,
            Properties.Resources.packManUp32,
            Properties.Resources.packManUp4,
            Properties.Resources.packManUp5
        };
        Image[] down = new Image[]
        {
            Properties.Resources.packManDown0,
            Properties.Resources.packManDown1,
            Properties.Resources.packManDown2,
            Properties.Resources.packManDown3,
            Properties.Resources.packManDown31,
            Properties.Resources.packManDown32,
            Properties.Resources.packManDown4,
            Properties.Resources.packManDown5
        };
        Image[] right = new Image[]
        {
            Properties.Resources.packManRight0,
            Properties.Resources.packManRight1,
            Properties.Resources.packManRight2,
            Properties.Resources.packManRight3,
            Properties.Resources.packManRight31,
            Properties.Resources.packManRight32,
            Properties.Resources.packManRight4,
            Properties.Resources.packManRight5
        };
        Image[] left = new Image[]
        {
            Properties.Resources.packManLeft0,
            Properties.Resources.packManLeft1,
            Properties.Resources.packManLeft2,
            Properties.Resources.packManLeft3,
            Properties.Resources.packManLeft31,
            Properties.Resources.packManLeft32,
            Properties.Resources.packManLeft4,
            Properties.Resources.packManLeft5
        };
        public Image[] Up
        {
            get { return up; }
        }
        public Image[] Down
        {
            get { return down; }
        }
        public Image[] Right
        {
            get { return right; }
        }
        public Image[] Left
        {
            get { return left; }
        }
    }
}
