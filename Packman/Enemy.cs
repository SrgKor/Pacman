using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    abstract class Enemy : Being        // Класс содержит метод приследования Пакмена и параметр "грустность".
    {
        protected Random random = new Random();

        public bool isSad;

        public bool isBaseLeft;             // Поле определяет покинута ли база.

        public void LeaveBase()
        {
            if (X > 146 && X < 195 && Y > 140 && Y < 182)
            {
                isBaseLeft = false;

                if (random.Next(100) < 50)      // замедление
                    return;
                Y--;
            }
            isBaseLeft = true;
        }

        public void RunTo(int packManX, int packManY)
        {
            if (isBaseLeft == false)
                return;

            // Замедление
            if (isSad && random.Next(1000) < 500)
                return;
                    
            if (!isSad && random.Next(1000) < 100)
                return;


            if (packManX > X)
                DirectionX = 1;
            else if (packManX < X)
                DirectionX = -1;

            if (packManY > Y)
                DirectionY = 1;
            else if (packManY < Y)
                DirectionY = -1;

            if (random.Next(5000) > 2500)
                DirectionX = 0;
            else
                DirectionY = 0;

            wantToX = x + DirectionX;
            wantToY = y + DirectionY;

            if (gameFieldMap.ThisPointIsRoad(wantToX, wantToY))
            {
                X = wantToX;
                Y = wantToY;

                if (isSad)
                    return;

                ManageImage(DirectionX, DirectionY);
            }
        }

        
        public abstract void ManageImage(int dirX, int dirY);
    }
}
