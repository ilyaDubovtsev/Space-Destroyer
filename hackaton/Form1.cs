using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace hackaton
{
    public partial class Form1 : Form
    {
        SoundPlayer music = new SoundPlayer("Sound.wav");
        bool left;
        bool right;
        int delta = 0;
        Painter painter;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (left & delta > -10)
                delta -= 1;
            if (right & delta < 10)
                delta += 1;
            if (!right & !left & delta != 0)
                delta -= delta / Math.Abs(delta);
            Game.Update();
<<<<<<< HEAD
            Game.GameCounter++;
            if (Game.GameCounter == 20)
            {
                Game.GameCounter = 0;
                Game.AddObject();
            }
            //Game.AddObject();
            pictureBox1.Image = painter.Paint(Game.gameObjects, Game.hero);
=======
            Game.hero.Move(delta);
            pictureBox1.Image = painter.Paint(Game.ReturnAllObjects());
>>>>>>> 36ac07349bec81d909d44648deeafaf3a8df64e3
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Game.Start();
            music.PlayLooping();
            painter = new Painter(pictureBox1, Game.BackGround);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                left = false;
            if (e.KeyCode == Keys.Right)
                right = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                left = true;
            if (e.KeyCode == Keys.Right)
                right = true;
        }
    }
}
