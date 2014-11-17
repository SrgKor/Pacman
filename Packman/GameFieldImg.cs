using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pacman
{
    class GameFieldImg
    {
        Image[] gameFields = new Image[]
        {
            Properties.Resources.gameField1,
            Properties.Resources.gameField2,
            Properties.Resources.gameField3,
            Properties.Resources.gameField4,
            Properties.Resources.gameField5     // 0..4 
        };

        public Image[] GameFields
        {
            get { return gameFields; }
        }
    }
}
