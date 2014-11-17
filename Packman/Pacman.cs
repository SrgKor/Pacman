using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;



namespace Pacman
{
    class Pacman : Being
    {
        public Pacman()         // Конструктор
        {
            currentImg = packmanImg.Right[3];
            X = defaultX;
            Y = defaultY;
            DirectionX = 1;
            DirectionY = 0;
            step = 4;
        }

        PacmanImg packmanImg = new PacmanImg();

        public short defaultX = 170;
        public short defaultY = 284;

        
        byte step;           // Пакмен двигается шагами (от 0 до 7)
        public byte Step
        {
            set {  step = value; }
        }

        byte numberOfSteps = 8;  // Количество кадров анимации.

        public byte lives;              // Количество  жизней.

        public void Run()
        {
            wantToX = x + directionX;
            wantToY = y + directionY;

            if (gameFieldMap.ThisPointIsRoad(wantToX, wantToY))
            {
                X = wantToX;
                Y = wantToY;
                ManageImage(DirectionX, DirectionY);
                PrevDirectionX = DirectionX;    // Сбросить значения предыдущих направлений за ненадобностью.
                PrevDirectionY = DirectionY;
            }
            else  if (gameFieldMap.ThisPointIsRoad(X + PrevDirectionX, Y + prevDirectionY))  // двигаться по предыдущему направлени
            {
                X += PrevDirectionX;
                Y += PrevDirectionY;
                ManageImage(PrevDirectionX, PrevDirectionY);
            }
        }

        public void ManageImage(int dirX, int dirY)
        {
                // считать шаги и менять картинку
                if (step == numberOfSteps)
                    step = 0;

                if (dirX == 1)
                    currentImg = packmanImg.Right[step++];
                else if (dirX == -1)
                    currentImg = packmanImg.Left[step++];
                else if (dirY == 1)
                    currentImg = packmanImg.Down[step++];
                else
                    currentImg = packmanImg.Up[step++];
        }

        
    }
}
