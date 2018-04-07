using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackaton
{
    class Commet : IGameObject
    {
        public Bitmap Sprite { get; }
        public Point Position { get; private set; }
        public int Priority => 3;
        public int HitboxRadius { get; }
        public int Speed => 5;
        public int Heals { get; set; }
        public int Damage => 25;

        public void SetNewPosition()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }

        public Commet(Bitmap sprite, Point startPosition)
        {
            this.Sprite = sprite;
            this.Position = startPosition;
            this.HitboxRadius = 28;
            this.Heals = 20;
        }
    }
}
