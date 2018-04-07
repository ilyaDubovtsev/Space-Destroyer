using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackaton
{
    class BigCommet : IGameObject
    {
        public Bitmap Sprite { get; }
        public Point Position { get; private set; }
        public int Priority => 2;
        public int HitboxRadius { get; }
        public int Speed => 5;
        public int Heals { get; set; }
        public int Damage  => 50;

        public void SetNewPosition()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }

        public BigCommet(Bitmap sprite, Point startPosition)
        {
            this.Sprite = sprite;
            this.Position = startPosition;
            this.HitboxRadius = 64;
            this.Heals = 30;
        }
    }
}
