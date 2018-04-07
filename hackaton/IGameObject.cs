﻿using System;
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
        int Priority { get; }
        int HitboxRadius { get; }
    }
}
