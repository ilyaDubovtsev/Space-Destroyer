using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace hackaton
{
    public interface IGameObject
    {
        Bitmap Sprite { get; }
        Point Position { get; }  //центр
        int Priority { get; } //приоритет рисования от 0
        int HitboxRadius { get; }
        int Speed { get; } // пикселей за тик
        int Heals { get; set; }
        int Damage { get; }
        void SetNewPosition();
    }
}
