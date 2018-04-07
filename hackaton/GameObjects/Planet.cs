using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackaton
{
    class Planet : IGameObject
    {
        public Bitmap Sprite { get; }
        public Point Position { get; private set; }
        public int Priority => 1;
        public int HitboxRadius { get; }
        public int Speed => 5;
        public int Heals
        {
            get => 1000;
            set { }
        }
        public int Damage => 1000;
        public void SetNewPosition()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }

        public Planet(Bitmap sprite, Point startPosition)
        {
            this.Sprite = sprite;
            this.Position = startPosition;
            this.HitboxRadius = 150;
        }
    }
}
