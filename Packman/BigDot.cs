using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pacman
{
    class BigDot
    {
        public BigDot(int x, int y)
        {
            CurrentImg = bigDotImg.BigDotShots[0];
            X = x;
            Y = y;
        }

        int x;
        int y;
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        Image currentImg;
        public Image CurrentImg         // Текущее изображение сущности
        {
            get { return currentImg; }
            set { currentImg = value; }
        }

        byte numberOfShot;              // Номер кадра для анимации.

        BigDotImg bigDotImg = new BigDotImg();

        public void Pulse()              // Мигание
        {
            numberOfShot++;
            if (numberOfShot == 6)
                numberOfShot = 0;

            CurrentImg = bigDotImg.BigDotShots[numberOfShot];
        }
    }
}
