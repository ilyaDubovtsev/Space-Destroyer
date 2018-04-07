using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackaton
{
    public class Planet : IGameObject
    {
        //private static Bitmap _sprite = (Bitmap) Image.FromFile("img\\Commet.png");
        public Bitmap Sprite => Game.PlanetSprite;
        public Point Position { get; private set; }
        public int Priority => 1;
        public int HitboxRadius { get; }
        public int Speed => 5;
        public int Heals
        {
            get => 1000;
            set { }
        }
        public int Damage => 5;
        public void SetNewPosition()
        {
            Position = new Point(Position.X, Position.Y + Speed);
        }

        public Planet(Point startPosition)
        {
            this.Position = startPosition;
            this.HitboxRadius = 32;
        }
    }
}
