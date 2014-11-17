using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace Pacman
{
    // Отрисовка игры (На UserControl).
    partial class View : UserControl
    {
        public View(Model model, ControllerMainForm controller)
        {
            InitializeComponent();

            this.model = model;
            this.controller = controller;
        }

        Model model;
        ControllerMainForm controller;
        
        // Переопределенный метод родительского класса UserControl, обработчик события Paint. Работает постоянно, перерисовывая UserControl.
        protected override void OnPaint(PaintEventArgs e)
        {
            if (model.gameState == GameState.SplashScreen)
                return;
            else if (model.gameState == GameState.Pause)
            {
                Draw(e);
                return;
            }
            else if (model.gameState == GameState.GameOver)
            {
                DisplayGameOver(e);
                return;
            }
            else if (model.gameState == GameState.GetReady)
            {
                Draw(e);
                DisplayGetReady(e);
                Invalidate();
            }
            else if (model.gameState == GameState.Play)
            {
                Draw(e);
                Thread.Sleep(model.Delay);
                Invalidate();
            }
        }

        private void Draw(PaintEventArgs e)    
        {
            DrawLittleDots(e);
            DrawBigDots(e);
            DrawGhosts(e);
            DrawPackman(e);
        }

        private void DrawLittleDots(PaintEventArgs e)   // Отрисовка очковых точек.
        {
            for (int i = 0; i < model.littleDots.listOfLittleDots.Count; i++ )
                e.Graphics.DrawImage(model.littleDots.ImageOfLittleDot,
                    new Point(model.littleDots.listOfLittleDots[i].x, model.littleDots.listOfLittleDots[i].y));
        }
        
        private void DrawPackman(PaintEventArgs e)  // Отрисовка Пакмена
        {
            e.Graphics.DrawImage(model.packman.CurrentImg, new Point(model.packman.X, model.packman.Y));
        }

        private void DrawGhosts(PaintEventArgs e)
        {
            foreach (Enemy ghost in model.ghosts)
            {
                e.Graphics.DrawImage(ghost.CurrentImg, new Point(ghost.X, ghost.Y));
            }
        }

        private void DrawBigDots(PaintEventArgs e)
        {
            foreach (BigDot item in model.bigDots)
            {
                e.Graphics.DrawImage(item.CurrentImg, new Point(item.X, item.Y));
            }
        }

        private void DisplayGameOver(PaintEventArgs e)
        {
            e.Graphics.DrawImage(Properties.Resources.Game_Over, new Point(123, 214));
        }

        private void DisplayGetReady(PaintEventArgs e)
        {
            e.Graphics.DrawImage(Properties.Resources.GetReady, new Point(130, 214));
        }

        private void View_KeyPress(object sender, KeyPressEventArgs e)  // Обработчик события KeyPress
        {
            controller.SpecifyDirectionForPackman(e.KeyChar);
        }
    }
}
