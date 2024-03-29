﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackaton
{
    public class Hero : IGameObject
    {
        public Bitmap Sprite { get; private set; }
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

        public Hero(Bitmap sprite, Point startPosition)
        {
            Sprite = sprite;
            Position = startPosition;
            HitboxRadius = 32;
            Heals = 100;
        }

        public void Move(int delta)
        {
            var newX = Position.X+delta;
            if (newX < 0)
                newX = 0;
            if (newX > 600)
                newX = 600;
            Position = new Point(newX, Position.Y);
        }

        public void ChangeSprite()
        {
            switch (Game.GameCounter % 3)
            {
                case 0:
                    Sprite = Game.PlayerSprite1;
                    break;
                case 1:
                    Sprite = Game.PlayerSprite2;
                    break;
                case 2:
                    Sprite = Game.PlayerSprite3;
                    break;
            }
        }

  
    }
}
