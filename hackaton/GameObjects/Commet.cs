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
        public Bitmap Sprite => Game.CometSprite;
        public Point Position { get; private set; }
        public int Priority => 3;
        public int HitboxRadius { get; }
        public int Speed => 10;
        public int Heals { get; set; }
        public int Damage => 20;

        public void SetNewPosition()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }

        public Commet(Point startPosition)
        {
            Position = startPosition;
            HitboxRadius = 14;
            Heals = 60;
        }
    }
}
