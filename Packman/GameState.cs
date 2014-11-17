using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    // Состояния игры
    enum GameState
    {
        SplashScreen,   // Заставка
        Play,           // Идет игра
        Pause,           // 
        NewLive,
        GameOver,        //
        GetReady
    }
}
