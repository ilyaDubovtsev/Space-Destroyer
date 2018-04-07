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
        public Bitmap Sprite { get; }
        public Point Position { get; }
        public int Priority { get; }
        public int HitboxRadius { get; }
        public int Speed { get; }
        public int Heals { get; set; }
        public int Damage { get; }
        public void SetNewPosition()
        {
            throw new NotImplementedException();
        }
    }
}
