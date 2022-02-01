using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackaton
{
    public class BigCommet : IGameObject
    {
        public Bitmap Sprite => Game.BigCometSprite;
        public Point Position { get; private set; }
        public int Priority => 2;
        public int HitboxRadius { get; }
        public int Speed => 15;
        public int Heals { get; set; }
        public int Damage  => 15;

        public void SetNewPosition()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }

        public BigCommet(Point startPosition)
        {
            this.Position = startPosition;
            this.HitboxRadius = 14;
            this.Heals = 40;
        }
    }
}
