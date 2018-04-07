﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace hackaton
{
    public static class Game
    {
        public static Bitmap CommetSprite = (Bitmap) Image.FromFile("img\\Commet.png");
        public static Bitmap BigCommetSprite = (Bitmap)Image.FromFile("img\\BigCommet.png");
        public static Bitmap PlanetSprite = (Bitmap)Image.FromFile("img\\Planet.png");
        public static Bitmap BulletSprite = (Bitmap) Image.FromFile("img\\Bullet.png");
        public static Bitmap BackGround;
        public static Hero hero;
        public static LinkedList<IGameObject> gameObjects;
        public static LinkedList<Bullet> Bullets;
        public static int Score;

        public static int GameCounter = 0; 

        public static void Start()
        {
            Score = 0;
            BackGround = (Bitmap) Image.FromFile("img\\Background.bmp");
            hero = new Hero((Bitmap) Image.FromFile("img\\Hero.png"), new Point(200, 500));
            gameObjects = new LinkedList<IGameObject>();
            Bullets = new LinkedList<Bullet>();
        }

        public static void AddBullet()
        {
            var bullet = new Bullet(hero.Position);
            Bullets.AddLast(bullet);
            gameObjects.AddLast(bullet);
        }

        public static void AddObject()
        {
            var r = new Random();
            var xRandom = r.Next(0, 400);
            switch (r.Next(0, 100) % 2)
            {
                case 0:
                    gameObjects.AddLast(new Commet(new Point(xRandom, -150)));
                    break;
                case 1:
                    gameObjects.AddLast(new BigCommet(new Point(xRandom, -150)));
                    break;
                //case 2:
                //    gameObjects.AddLast(new Planet(new Point(r.Next(0, 1) == 1 ? 0 : 400, -150)));
                //    break;
            }
        }

        public static void Update()
        {
            var forClering = new List<IGameObject>();
            foreach (var gameObject in gameObjects)
            {
                gameObject.SetNewPosition();
                if (PositionCheck(gameObject))
                    forClering.Add(gameObject);
                if (IsBumpToHero(gameObject))
                {
                    forClering.Add(gameObject);
                    hero.Heals -= gameObject.Damage;
                    if (hero.Heals <= 0)
                        GameOver();
                }
                if (!(gameObject is Bullet))
                {
                    foreach (var bullet in Bullets)
                    {
                        if (IsShooted(bullet, gameObject))
                        {
                            gameObject.Heals -= bullet.Damage;
                            if (gameObject.Heals <= 0)
                            {
                                Score++;
                                forClering.Add(gameObject);
                            }
                            forClering.Add(bullet);
                        }
                    }
                }
            }

            foreach (var gameObject in forClering)
            {
                gameObjects.Remove(gameObject);
                if (gameObject is Bullet)
                    Bullets.Remove(gameObject as Bullet);
            }
        }

        public static IEnumerable<IGameObject> ReturnAllObjects()
        {
            var result = gameObjects.ToList();
            result.Add(hero);
            return result;
        }

        private static bool PositionCheck(IGameObject gameObject)
        {
            return (gameObject.Position.Y > 650 || gameObject is Bullet && gameObject.Position.Y < -10);
        }

        private static bool IsBumpToHero(IGameObject ob)
        {
            if (ob is Bullet) return false;
            var deltaX = hero.Position.X - ob.Position.X;
            var deltaY = hero.Position.Y - ob.Position.Y;
            var distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            return distance < ob.HitboxRadius + hero.HitboxRadius;
        }

        private static bool IsShooted(Bullet bullet, IGameObject gameObject)
        {
            var deltaX = bullet.Position.X - gameObject.Position.X;
            var deltaY = bullet.Position.Y - gameObject.Position.Y;
            var distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            return distance < gameObject.HitboxRadius + 1;
        }

        public static void GameOver()
        {
            gameObjects = new LinkedList<IGameObject>();
            hero = new Hero((Bitmap)Image.FromFile("img\\Hero.png"), new Point(200, 300));
        }
    }
}
