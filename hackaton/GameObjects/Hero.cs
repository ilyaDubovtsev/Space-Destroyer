using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackaton
{
    class Hero : IGameObject
    {
        public Bitmap Sprite { get; }
        public Point Position { get; private set; }
        public int Priority => 100;
        public int HitboxRadius { get; }
        public int Speed => 0;
        public int Heals { get; set; }
        public int Damage => 0;
        public void SetNewPosition()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }

        public Hero(Bitmap sprite, Point startPosition, int hitboxRadius)
        {
            this.Sprite = sprite;
            this.Position = startPosition;
            this.HitboxRadius = hitboxRadius;
            this.Heals = 100;

        }
    }
}
