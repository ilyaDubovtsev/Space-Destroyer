using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackaton
{
    class Bullet : IGameObject
    {
        public Bitmap Sprite => Game.BulletSprite;
        public Point Position { get; private set; }
        public int Priority => 99;
        public int HitboxRadius => 1;
        public int Speed => -10;
        public int Heals
        {
            get => 100;
            set {}
        }

        public int Damage => 5;

        public void SetNewPosition()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }

        public Bullet(Point startPosition)
        {
            this.Position = startPosition;
        }
    }
}
