using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Media;
using System.Timers;

namespace Pacman
{
    delegate void MyDelegate();
    
    // Model описывает бизнес - логику игры.
    class Model
    {
        // События
        #region События
        public event MyDelegate scoreChangedEvent;
        public event MyDelegate livesChangedEvent;
        public event MyDelegate gameOverEvent;
        public event MyDelegate newLevelEvent;
        //public event MyDelegate 
        #endregion

        // Параметры игры
        #region Параметры игры
        public byte Delay = 10;               // Величина задержки в работе потока по умолчанию.
        public GameState gameState;          // Состояние игры.
        public long score;                   // Количество набранных очков.
        public byte level = 1;
        #endregion

        // Сущности игры
        #region Сущности игры
        public List<Enemy> ghosts;
        public List<BigDot> bigDots;
        public Pacman packman;
        public LittleDots littleDots;
        public RedGhost redGhost;
        public YellowGhost yellowGhost;
        public PinkGhost pinkGhost;
        public CyanGhost cyanGhost;
        public BigDot bigDot1, bigDot2, bigDot3, bigDot4;
        #endregion
        
        
        // Таймер засекает время пребывания привидений в "грустном" состоянии.
        System.Timers.Timer timerOfGhostsBeeingSad;

        // Звучки
        #region Звучки
        SoundPlayer getReadySound = new SoundPlayer(Properties.Resources.GetReadySound);
        SoundPlayer gameOverSound = new SoundPlayer(Properties.Resources.GameOverSound);
        SoundPlayer packManDeathSound = new SoundPlayer(Properties.Resources.PackManDeathSound);
        SoundPlayer eatSound = new SoundPlayer(Properties.Resources.eatcycle);
        SoundPlayer eatGhost = new SoundPlayer(Properties.Resources.eatGhost);
        #endregion

        public void InitializeNewGame()                // Создание сущностей и установка основных параметров по нулям.
        {
            packman = new Pacman();

            ghosts = new List<Enemy>();
            redGhost = new RedGhost();
            yellowGhost = new YellowGhost();
            pinkGhost = new PinkGhost();
            cyanGhost = new CyanGhost();
            ghosts.Add(redGhost);
            ghosts.Add(yellowGhost);
            ghosts.Add(pinkGhost);
            ghosts.Add(cyanGhost);

            BigDodsCreating();

            littleDots = new LittleDots();

            score = 0;
            scoreChangedEvent();
            packman.lives = 3;
            livesChangedEvent();
            level = 1;
            newLevelEvent();

            EverybodyToHisDefaultPosition();

            gameState = GameState.Play;
        }

        private void BigDodsCreating()                          // Создание больших точек и списка.
        {
            bigDots = new List<BigDot>();
            bigDot1 = new BigDot(25, 49);
            bigDot2 = new BigDot(25, 288);
            bigDot3 = new BigDot(325, 49);
            bigDot4 = new BigDot(325, 288);
            bigDots.Add(bigDot1);
            bigDots.Add(bigDot2);
            bigDots.Add(bigDot3);
            bigDots.Add(bigDot4);
        }

        private void EverybodyToHisDefaultPosition()            // Расстановка сущностей на исходные позиции.
        {
            packman.X = packman.defaultX;
            packman.Y = packman.defaultY;
            packman.DirectionX = -1;
            packman.DirectionY = 0;
            packman.Step = 4;
            packman.ManageImage(packman.DirectionX, packman.DirectionY);
            redGhost.X = redGhost.defaultX;
            redGhost.Y = redGhost.defaultY;
            yellowGhost.X = yellowGhost.defaultX;
            yellowGhost.Y = yellowGhost.defaultY;
            pinkGhost.X = pinkGhost.defaultX;
            pinkGhost.Y = pinkGhost.defaultY;
            cyanGhost.X = cyanGhost.defaultX;
            cyanGhost.Y = cyanGhost.defaultY;
            getReadySound.Play();
        }

        private void NewLevel()                                       // Переход на новый уровень.
        {
            Thread.Sleep(400);
            level++;
            newLevelEvent();
            littleDots = new LittleDots();
            BigDodsCreating();
            EverybodyToHisDefaultPosition();
            gameState = GameState.GetReady;
            getReadySound.Play();
            GetReadyDelay();
            gameState = GameState.Play;
        }

        private void GetReady()
        {
            gameState = GameState.GetReady;
            GetReadyDelay();
            gameState = GameState.Play;
        }

        private void GameOver()
        {
            gameOverSound.Play();
            gameState = GameState.GameOver;
            gameOverEvent();
        }

        public void Play()                                   // Все игровые процессы крутятся здесь в цикле
        {
            byte step = 0;

            GetReady();
           
            while (gameState == GameState.Play)
            {
                step++;
                if (step == 11)
                    step = 0;
                Thread.Sleep(Delay);

                foreach (Enemy ghost in ghosts)
                {
                    ghost.LeaveBase();
                    ghost.RunTo(packman.X, packman.Y);
                    ghost.Teleportation();
                }
               

                cyanGhost.RunTo(packman.X, packman.Y);
                
                yellowGhost.RunTo(packman.X, packman.Y);
                redGhost.RunTo(packman.X, packman.Y);

                packman.Run();
                packman.Teleportation();

                LittleDotsEating();
                KeepTrackOfCollisions();
                BigDotsPulsing(step);
                KeepTrackOfBigDotsEating();
            }
        }

        private void KeepTrackOfBigDotsEating()                 // Отслеживание поедания больших точек.
        {
            for (int i = 0; i < bigDots.Count; i++)
            {
                if (Math.Abs(packman.MiddleX - bigDots[i].X) < 8 && Math.Abs(packman.MiddleY - bigDots[i].Y) < 8)
                {
                    bigDots.Remove(bigDots[i]);
                    score += 90;
                    scoreChangedEvent();
                    foreach (Enemy item in ghosts)
                    {
                        item.isSad = true;
                        item.CurrentImg = Properties.Resources.SadGhost;
                    }
                    CreateTimer();
                }
            }
        }
        
        private void CreateTimer()                          // Таймер пребывания привидений в "подавленном" состоянии.
        {
            if (timerOfGhostsBeeingSad != null)
                timerOfGhostsBeeingSad.Enabled = false;

            timerOfGhostsBeeingSad = new System.Timers.Timer();
            timerOfGhostsBeeingSad.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timerOfGhostsBeeingSad.Interval = 10000; //Устанавливаем интервал 
            timerOfGhostsBeeingSad.Enabled = true; //Вкючаем таймер.
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            foreach (Enemy item in ghosts)
            {
                item.isSad = false;
                item.ManageImage(1, 0);
            }
            timerOfGhostsBeeingSad.Enabled = false;
        }

        private void BigDotsPulsing(byte step)                      // Мигание больших точек.
        {
            switch (step)
            {
                case 0:
                    bigDot1.Pulse();
                    break;
                case 3:
                    bigDot2.Pulse();
                    break;
                case 6:
                    bigDot3.Pulse();
                    break;
                case 10:
                    bigDot4.Pulse();
                    break;
                default:
                    break;
            }
        }

        private void LittleDotsEating()     // Поедание очковых точек.
        {
            for (int i = 0; i < littleDots.listOfLittleDots.Count; i++)
            {
                int dotsX = littleDots.listOfLittleDots[i].x;
                int dotsY = littleDots.listOfLittleDots[i].y;
                if (Math.Abs(packman.MiddleX - dotsX) < 2
                    && Math.Abs(packman.MiddleY - dotsY) < 2)
                {
                    littleDots.listOfLittleDots.Remove(littleDots.listOfLittleDots[i]);
                    score += 10;
                    scoreChangedEvent();
                    if (Math.IEEERemainder(score, 20) == 0)
                        eatSound.Play();
                    if (littleDots.listOfLittleDots.Count == 0)
                    {
                        foreach (Enemy item in ghosts)
                        {
                            item.isSad = false;
                        }
                        NewLevel();
                    }
                }
            }
        }

        private void KeepTrackOfCollisions()          // Столкновение Пакмена с привидениями.
        {
            foreach (Enemy ghost in ghosts)
            {
                if (Math.Abs(packman.MiddleX - ghost.MiddleX) < 15 && Math.Abs(packman.MiddleY - ghost.MiddleY) < 15)
                {
                    if (ghost.isSad)
                    {
                        EatSadGhost(ghost);
                        continue;
                    }

                    packman.lives--;
                    livesChangedEvent();
                    packManDeathSound.Play();
                    PackManDeathDelay();
                    DecideWhatToDoAfterCollision();
                }
            }
        }

        private void EatSadGhost(Enemy ghost)                      // Поедание привидений.
        {   
                    score += 100;
                    scoreChangedEvent();
                    ghost.isSad = false;
                    ghost.ManageImage(1, 0);
                    ghost.X = ghost.defaultX;
                    ghost.Y = ghost.defaultY;
                    
                    eatGhost.Play();
                    Thread.Sleep(470);
        }

        private void DecideWhatToDoAfterCollision()             // Действия после столкновения пакмена с привид.
        {
            if (packman.lives > 0)
            {
                EverybodyToHisDefaultPosition();
                GetReady();
            }
            else
                GameOver();
        }

        private static void GetReadyDelay()
        {
            Thread.Sleep(4200);
        }
        private static void PackManDeathDelay()
        {
            Thread.Sleep(2000);
        }

    }
}
