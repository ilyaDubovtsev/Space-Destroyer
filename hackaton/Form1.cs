using System;
using System.Windows.Forms;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace hackaton
{
    public partial class Form1 : Form
    {
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
            Game.GameCounter++;
            if (Game.GameCounter == 50)
            {
                Game.AddObject();
                Game.GameCounter = 0;
            }

            if (Game.GameCounter % 5 == 0)
            {
                Game.Hero.ChangeSprite();
                Game.AddBullet();
            }
            Game.Hero.Move(delta);
            pictureBox1.Image = painter.Paint(Game.ReturnAllObjects());
            Score.Text = Game.Score.ToString();
            Heals.Text = Game.Hero.Heals.ToString();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Game.Timer = timer1;
            Game.Start();
            Task.Run(() => Game.music.PlayLooping());
            painter = new Painter(pictureBox1, Game.BackGround);
            Heals.Text = Game.Hero.Heals.ToString();
            var builder = new StringBuilder();
            int index = 1;
            foreach (var value in Game.LoadOldHeightScore())
            {
                builder.Append(index);
                builder.Append(".  ");
                builder.Append(value);
                builder.Append('\n');
                index++;
            }
            TopScoreLabel.Text = builder.ToString();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                left = false;
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                right = false; 
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                left = true;
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                right = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game.Start();
            var builder = new StringBuilder();
            int index = 1;
            foreach (var value in Game.LoadOldHeightScore())
            {
                builder.Append(index);
                builder.Append(".  ");
                builder.Append(value);
                builder.Append('\n');
                index++;
            }
            builder.Remove(builder.Length - 1, 1);
            TopScoreLabel.Text = builder.ToString();
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }
    }
}
