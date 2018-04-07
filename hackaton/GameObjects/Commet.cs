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
        public Point Position { get; }
        public int Priority => 3;
        public int HitboxRadius { get; }
        public int Speed => 10;
        public int Heals { get; set; }
        public int Damage => 25;
        public void SetNewPosition()
        {
            throw new NotImplementedException();
        }

        public Commet(Bitmap sprite, Point startPosition, int hitboxRadius)
        {
            this.Sprite = sprite;
            this.Position = Position;
            this.HitboxRadius = hitboxRadius;
            this.Heals = 20;
        }
    }
}
