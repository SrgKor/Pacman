using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;

[assembly: CLSCompliant(true)]

namespace Pacman
{
    delegate void CrossThreadWorkDelegate();    // делегат для кросспоточной работы.
    
    // Контроллер взаимодействует с пользователем и осуществляет управление игрой.
    public partial class ControllerMainForm : Form
    {
        Thread playThread;      // Новый поток для игры. (В основном работает форма.)
        Model model;
        View view;
        GameFieldImg gameFieldImage;

        public ControllerMainForm()         // Конструктор
        {
            InitializeComponent();

            model = new Model();
            view = new View(model,this);
            this.Controls.Add(view);        // Добавляем на форму UserControl

            model.gameState = GameState.SplashScreen;

            gameFieldImage = new GameFieldImg();

            model.scoreChangedEvent += new MyDelegate(DisplayScore);
            model.livesChangedEvent += new MyDelegate(DisplayLives);
            model.gameOverEvent += new MyDelegate(SuspendTheThread);
            model.newLevelEvent += new MyDelegate(DisplayLevelAndSetGameFieldForThisLevel);
        }

        // Запуск игры через меню. Создание потока и запуск в нем метода model.Play().
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (playThread != null)
            {
                try
                {
                    playThread.Suspend();
                }
                catch (ThreadStateException)
                {
                }
            }

            view.BackgroundImage = Properties.Resources.gameField1;
            model.InitializeNewGame();
            model.gameState = GameState.Play;
            playThread = new Thread(model.Play);
            playThread.IsBackground = true;
            playThread.Start();
        }

        //  Это обработчик события KeyPress UserControla. Задает направление движения для Пакмена.
        public void SpecifyDirectionForPackman(char button)     
        {
            model.packman.PrevDirectionX = model.packman.DirectionX;
            model.packman.PrevDirectionY = model.packman.DirectionY;

            switch (button)
            {
                case '8':
                    {
                        model.packman.DirectionY = -1;
                        model.packman.DirectionX = 0;
                    }   break;
                case '5':
                    {
                        model.packman.DirectionY = 1;
                        model.packman.DirectionX = 0;
                    }   break;
                case '4':
                    {
                        model.packman.DirectionY = 0;
                        model.packman.DirectionX = -1;
                    }   break;
                case '6':
                    {
                        model.packman.DirectionY = 0;
                        model.packman.DirectionX = 1;
                    }   break;
            }
        }

        // Пауза
        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)           // Обработчик Play/Pause
        {
            if (playThread == null)
                return;
            if (playThread != null && model.gameState == GameState.Play)    // если идет игра
            {
                model.gameState = GameState.Pause;
                try
                {
                    playThread.Suspend();
                }
                catch(ThreadStateException)
                {
                }
            }
            else if (model.gameState == GameState.Pause)
            {
                model.gameState = GameState.Play;
                try
                {
                    playThread.Resume();
                }
                catch(ThreadStateException) { }
                view.Invalidate();
            }
        }

        private void DisplayScore()                 // Отображает кол-во набранных очков на панели статуса.
        {
            Invoke(new CrossThreadWorkDelegate(SetValueOfScore));
        }

        private void SetValueOfScore()
        {
            toolStripStatusLabelScore.Text = "Score: " + model.score.
                ToString(new System.Globalization.CultureInfo("En", true));
        }

        private void DisplayLives()
        {
            Invoke(new CrossThreadWorkDelegate(SetValueOfLives));
        }
        private void SetValueOfLives()
        {
            toolStripStatusLabelLives.Text = "Lives: " + model.packman.lives.
                ToString(new System.Globalization.CultureInfo("En", true));
        }
        private void DisplayLevelAndSetGameFieldForThisLevel()
        {
            Invoke(new CrossThreadWorkDelegate(SetLevelValue));
            Invoke(new CrossThreadWorkDelegate(SetGameField));

        }
        private void SetLevelValue()
        {
            toolStripStatusLabelLevel.Text = "Level: " + model.level.
                ToString(new System.Globalization.CultureInfo("En",true));
        }
        private void SetGameField() // Устанавливает игровое поле
        {
            int pictureIndex = -1;
            for (int i = 0; i < model.level; i++)
            {
                pictureIndex++;
                if (pictureIndex == 5)
                    pictureIndex = 0;
            }

            view.BackgroundImage = gameFieldImage.GameFields[pictureIndex];
            
        }

        private void SuspendTheThread()
        {
            try {playThread.Suspend();}
            catch(ThreadStateException) { }
        }

        #region Завершение работы программы закрытием окна и через меню.

        
        private void ControllerMainForm_FormClosing(object sender, FormClosingEventArgs e)          // Обработчик события FormClosing.
        {
            if (model.gameState == GameState.GameOver)
            {
                FormClosingInStateGameOver(e);
                return;
            }

            if (playThread != null) // Если поток существует, перед закрытием формы его нужно остановить.
                {
                    model.gameState = GameState.Pause;
                    try
                    {playThread.Suspend();}
                    catch(ThreadStateException) {}
                }

            DialogResult dialogResult;
            dialogResult = MessageBox.Show("Close the game?","Game closing",MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly);
            if (dialogResult == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                if (playThread != null)
                {
                    try
                    {playThread.Resume();}
                    catch(ThreadStateException) { }
                   
                    model.gameState = GameState.Play;
                    view.Invalidate();
                }
            }
        }

        private void FormClosingInStateGameOver(FormClosingEventArgs e) // Завершение в состоянии Game Over
        {
            DialogResult dialogResult;
            dialogResult = MessageBox.Show("Close the game?", "Game closing", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (dialogResult == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        
        // Завершение работы через меню.
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (model.gameState == GameState.GetReady)
                return;
            if (model.gameState != GameState.Pause)
                pauseToolStripMenuItem_Click(sender, e);
            DialogResult dr;
            dr = MessageBox.Show("Enjoy the classic Pacman game!" + 
                "\n\nUse 4, 5, 6 and 8 keys to control." +
                "\n\n Developer: Sergey Korolev.", "Pacman 1.0", MessageBoxButtons.OK, 
                MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (dr == DialogResult.OK)
                pauseToolStripMenuItem_Click(sender, e);
        }


        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.Delay = 5;
        }

        private void highToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.Delay = 1;
        }

        private void lowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.Delay = 15;
        }

        private void moderateSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.Delay = 10;
        }
    }
}
