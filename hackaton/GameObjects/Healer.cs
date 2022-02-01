using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackaton
{
    public class Healer : IGameObject
    {
        public Bitmap Sprite => Game.HealerSprite;
        public Point Position { get; private set; }
        public int Priority => 3;
        public int HitboxRadius { get; }
        public int Speed => 25;
        public int Heals { get => 1000;
            set { } }
        public int Damage => -20;

        public void SetNewPosition()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }

        public Healer(Point startPosition)
        {
            Position = startPosition;
            HitboxRadius = 14;
        }
    }
}
