using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    // Карта игрового поля.
    // Представляет собой матрицу 360х420 пикс.
    // Значение '0' соответствует стенам
    //          '1' -- дорогам ("рельсы" для верхнего левого угла картинки сущности)
    //          '2' -- дорогам без очковых точек

    // Класс реализует паттерн Singleton.

    sealed class GameFieldMap  
    {
        private GameFieldMap()
        {
            paths = new byte[width, height];
            ChartRoads();
        }
        private static GameFieldMap gameFieldMap = new GameFieldMap();

        public static GameFieldMap CreateSingleton()
        {
            return gameFieldMap;
        }
        
        public byte[,] paths;     
        public  int width = 360;
        public  int height = 420;

        public bool ThisPointIsRoad(int x, int y)  // Определяет является ли данная точка дорогой.
        {
            if (gameFieldMap.paths[x, y] == 1 || gameFieldMap.paths[x, y] == 2)
                return true;
            else
                return false;
        }
        
        private void ChartRoads()           // Нанесение на карту дорог.
        {
            //1Г
            for (int i = 20; i <= 152; i++)
                paths[i, 20] = 1;
            //2Г
            for (int i = 188; i <= 320; i++)
                paths[i, 20] = 1;
            //3Г
            for (int i = 20; i <= 320; i++)
                paths[i, 68] = 1;
            //4Г
            for (int i = 20; i <= 80; i++)
            {
                paths[i, 104] = 1;
            }
            //7Г 259;104 - 320;104
            for (int i = 260; i <= 320; i++)
            {
                paths[i, 104] = 1;
            }
            //8Г 116;140 - 224;140
            for (int i = 116; i <= 224; i++)
            {
                paths[i, 140] = 2;
            }
            //9Г 0;176 - 116;176
            for (int i = 1; i <= 116; i++)
            {
                paths[i, 176] = 2;
            }
            //10Г 224;176 - 360;176
            for (int i = 224; i <= 340; i++)
            {
                paths[i, 176] = 2;
            }
            //11Г 116;212 - 224;212
            for (int i = 116; i <= 224; i++)
            {
                paths[i, 212] = 2;
            }
            //16Г 296;284 - 320;284
            for (int i = 296; i <= 320; i++)
            {
                paths[i, 284] = 1;
            }
            //17Г 20; 320 - 80;320
            for (int i = 20; i <= 80; i++)
            {
                paths[i, 320] = 1;
            }
            //18Г 116;320 - 152; 320
            for (int i = 116; i <= 152; i++)
            {
                paths[i, 320] = 1;
            }
            //19Г 188;320 - 224;320
            for (int i = 188; i <= 224; i++)
            {
                paths[i, 320] = 1;
            }
            //20Г 260;320 - 320;320
            for (int i = 260; i <= 320; i++)
            {
                paths[i, 320] = 1;
            }
            //21Г 20;356 - 320;356
            for (int i = 20; i <= 320; i++)
            {
                paths[i, 356] = 1;
            }
            //1В
            for (int i = 20; i <= 104; i++)
                paths[20, i] = 1;
            //2B
            for (int i = 20; i <= 320; i++)
            {
                paths[80, i] = 1;
            }
            //3B 20;248 - 20;284
            for (int i = 248; i <= 284; i++)
            {
                paths[20, i] = 1;
            }
            //4B 20;320 - 20;356
            for (int i = 320; i <= 356; i++)
            {
                paths[20, i] = 1;
            }
            //5B 44;284 - 44;320
            for (int i = 284; i <= 320; i++)
            {
                paths[44, i] = 1;
            }
            //6B 116;68 - 116;104
            for (int i = 68; i <= 104; i++)
            {
                paths[116, i] = 1;
            }
            //7B 152;20 - 152;67
            for (int i = 20; i <= 67; i++)
            {
                paths[152, i] = 1;
            }
            //8B 187;20 - 187;67
            for (int i = 20; i <= 67; i++)
            {
                paths[188, i] = 1;
            }
            //9B 224; 68 - 224;104
            for (int i = 68; i <= 104; i++)
            {
                paths[224, i] = 1;
            }
            //10B 260;20 - 260;320
            for (int i = 20; i <= 320; i++)
            {
                paths[260, i] = 1;
            }
            //11B 320;20 - 320;104
            for (int i = 20; i <= 104; i++)
            {
                paths[320, i] = 1;
            }
            //12B 152;104 - 152;140
            for (int i = 104; i <= 140; i++)
            {
                paths[152, i] = 2;
            }
            //13B 188;104 - 188;140
            for (int i = 104; i <= 140; i++)
            {
                paths[188, i] = 2;
            }
            //14В 116;140 - 116;248
            for (int i = 140; i <= 248; i++)
            {
                paths[116, i] = 2;
            }
            //15B 224;140 - 224-248
            for (int i = 140; i <= 248; i++)
            {
                paths[224, i] = 2;
            }
            //16B 116;284 - 116;320
            for (int i = 284; i <= 320; i++)
            {
                paths[116, i] = 1;
            }
            //17B 152;248 - 152;284
            for (int i = 248; i <= 284; i++)
            {
                paths[152, i] = 1;
            }
            //18B 188;248 - 188;284
            for (int i = 248; i <= 284; i++)
            {
                paths[188, i] = 1;
            }
            //19B 224;284 - 224;320
            for (int i = 284; i <= 320; i++)
            {
                paths[224, i] = 1;
            }
            //20B 296;284 - 296;320
            for (int i = 284; i <= 320; i++)
            {
                paths[296, i] = 1;
            }
            //21B 320;248 - 320;284
            for (int i = 248; i <= 284; i++)
            {
                paths[320, i] = 1;
            }
            //22B 152;320 - 152;356
            for (int i = 320; i <= 356; i++)
            {
                paths[152, i] = 1;
            }
            //23B 188;320 - 188;365
            for (int i = 320; i <= 356; i++)
            {
                paths[188, i] = 1;
            }
            //24B 320;320 - 320;365
            for (int i = 320; i <= 356; i++)
            {
                paths[320, i] = 1;
            }
            //5Г 116;104 - 152;104
            for (int i = 116; i <= 152; i++)
            {
                paths[i, 104] = 1;
            }
            //6Г 188;104 - 224;104
            for (int i = 188; i <= 224; i++)
            {
                paths[i, 104] = 1;
            }
            //12Г 20;248 - 152;248
            for (int i = 20; i <= 152; i++)
            {
                paths[i, 248] = 1;
            }
            //13Г 188; 248 - 320;248
            for (int i = 188; i <= 320; i++)
            {
                paths[i, 248] = 1;
            }
            //14Г 20; 284 - 44; 284
            for (int i = 20; i <= 44; i++)
            {
                paths[i, 284] = 1;
            }
            //15Г 80;284 - 260;284
            for (int i = 80; i <= 260; i++)
            {
                paths[i, 284] = 1;
            }
            
            //База привидений

            
            for (int i = 165; i <= 175; i++)
                for (int j = 140; j <= 180; j++)
                    paths[i, j] = 2;
               
            //147 - 194 -- 170 - 181
            for (int i = 147; i <= 194; i++)
                for (int j = 170; j <= 181; j++)
                    paths[i, j] = 2;
        }
    }
}
