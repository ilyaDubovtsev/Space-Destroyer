using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hackaton
{
    public partial class Form1 : Form
    {
        Painter painter;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Game.Update();
            pictureBox1.Image = painter.Paint(Game.ReturnAllObjects());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Game.Start();
            painter = new Painter(pictureBox1, Game.BackGround);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                Game.hero.Move(-3);
            if (e.KeyCode == Keys.Right)
                Game.hero.Move(3);
        }
    }
}
