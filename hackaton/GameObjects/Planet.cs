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
        public Point Position { get; }
        public int Priority => 1;
        public int HitboxRadius { get; }
        public int Speed => 10;
        public int Heals
        {
            get => 1000;
            set { }
        }
        public int Damage => 1000;
        public void SetNewPosition()
        {
            throw new NotImplementedException();
        }

        public Planet(Bitmap sprite, Point startPosition, int hitboxRadius)
        {
            this.Sprite = sprite;
            this.Position = startPosition;
            this.HitboxRadius = hitboxRadius;
        }
    }
}
