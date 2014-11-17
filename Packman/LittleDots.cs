using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pacman
{
    // Класс описывает маленькие "очковые" точки на игровом поле.
    // Объект класса будет представлять собой список таких точек, заданных своими координатами. 
    // А так же он будет содержать изображение точки. Т.о. было решено отойти от схемы создания других объектов,
    // в которой за изображение отвечает отдельный класс. Кроме того здесь мы будем иметь только один объект, отвечающий
    // за все "очковые" точки.
    class LittleDots
    {
        public LittleDots() // Конструктор
        {
            listOfLittleDots = new List<LittleDotKoords>();     //Создание списка в куче.
            gameFieldMap = GameFieldMap.CreateSingleton();
            FillListOfLittleDots();
        }

        public List<LittleDotKoords> listOfLittleDots;      // Список

        public Image ImageOfLittleDot = Properties.Resources.LittleDot;

        private GameFieldMap gameFieldMap;

        public void FillListOfLittleDots()     // Заполнение списка парами координат.
        {
            for (int i = 20; i <= 320; i+=12)
                for (int j = 20; j <= 356; j+=12)
                    if(gameFieldMap.paths[i, j] == 1)
                        listOfLittleDots.Add(new LittleDotKoords(i + 9, j + 9) );
               
           
        }

        public struct LittleDotKoords  // Пара координат, задающая точку.
        {
            public LittleDotKoords(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public int x, y;
        }
    }

    
}
