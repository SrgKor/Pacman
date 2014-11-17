using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pacman
{
    abstract class Being        // Класс описывает изображение сущности, карту, координаты и направления, метод телепорт.
    {
        protected Image currentImg;
        public Image CurrentImg         // Текущее изображение сущности
        {
            get { return currentImg; }
            set { currentImg = value; }
        }

        public GameFieldMap gameFieldMap = GameFieldMap.CreateSingleton();    // Ссылка на объект "Карта игрового поля".

        #region Координаты,  направления движения. 

        protected int x, y;       // Текущие координаты

        public short defaultX, defaultY;

        protected int wantToX, wantToY; // Предполагаемые следующие координаты

        public int X
        {
            get { return x; }
            set
            {
                if (value >= 20 && value <= 320) x = value;
            }
        }
        public int Y
        {
            get { return y; }
            set
            {
                if (value >= 20 && value <= 356) y = value;
            }
        }

        public int MiddleX // Координата x середины картинки.
        {
            get { return x + 10; }
        }
        public int MiddleY  // Координата y середины картинки.
        {
            get { return y + 10; }
        }

        protected int directionX, directionY;             // Направление движения
        protected int prevDirectionX, prevDirectionY;     // Предыдущее направление движения

        public int DirectionX
        {
            get { return directionX; }
            set
            {
                if (value == -1 || value == 1 || value == 0)
                    directionX = value;
                else
                    directionX = 0;
            }
        }
        public int DirectionY
        {
            get { return directionY; }
            set
            {
                if (value == -1 || value == 1 || value == 0)
                    directionY = value;
                else
                    directionY = 0;
            }
        }
        public int PrevDirectionX
        {
            get { return prevDirectionX; }
            set
            {
                if (value == -1 || value == 1 || value == 0)
                    prevDirectionX = value;
                else
                    prevDirectionX = 0;
            }
        }
        public int PrevDirectionY
        {
            get { return prevDirectionY; }
            set
            {
                if (value == -1 || value == 1 || value == 0)
                    prevDirectionY = value;
                else
                    prevDirectionY = 0;
            }
        }

        #endregion

        public void Teleportation()     // Телепортация на противоположную сторону игрового поля
        {
            if (Y != 176)
                return;
            if (X <= 20 && DirectionX == -1)
                X = 320;
            else if (X >= 320 && DirectionX == 1)
                X = 20;
        }
    }
}
