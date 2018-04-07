using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackaton
{
    public class Commet : IGameObject
    {
        //private static Bitmap _sprite = (Bitmap) Image.FromFile("img\\Commet.png");
        public Bitmap Sprite => Game.CommetSprite;
        public Point Position { get; private set; }
        public int Priority => 3;
        public int HitboxRadius { get; }
        public int Speed => 5;
        public int Heals { get; set; }
        public int Damage => 15;

        public void SetNewPosition()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }

        public Commet(Point startPosition)
        {
            this.Position = startPosition;
            this.HitboxRadius = 14;
            this.Heals = 40;
        }
    }
}
