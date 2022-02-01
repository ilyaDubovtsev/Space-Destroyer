using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Media;

namespace hackaton
{
    public static class Game
    {
        public static readonly Bitmap CometSprite = (Bitmap)Image.FromFile("img\\Comet.png");
        public static readonly Bitmap BigCometSprite = (Bitmap)Image.FromFile("img\\BigComet.png");
        public static readonly Bitmap BulletSprite = (Bitmap)Image.FromFile("img\\Bullet.png");
        public static readonly Bitmap PlayerSprite1 = (Bitmap)Image.FromFile("img\\Player1.png");
        public static readonly Bitmap PlayerSprite2 = (Bitmap)Image.FromFile("img\\Player2.png");
        public static readonly Bitmap PlayerSprite3 = (Bitmap)Image.FromFile("img\\Player3.png");
        public static readonly Bitmap HealerSprite = (Bitmap)Image.FromFile("img\\healUp.png");

        public static Bitmap BackGround;
        public static Hero Hero;
        private static LinkedList<IGameObject> gameObjects;
        private static LinkedList<Bullet> bullets;
        public static int Score;
        private static readonly Random r = new Random();
        public static Timer Timer;
        public static readonly SoundPlayer music = new SoundPlayer("sound\\Music.wav");

        /*Additional Sprite*/
        //public static Bitmap BrokenBigCometSprite = (Bitmap)Image.FromFile("img\\BrokenBigComet.png");

        /*Additional soundtracks*/
        //static SoundPlayer hit = new SoundPlayer("Sound.wav");
        //static SoundPlayer hitSound = new SoundPlayer("sound\\Hit.wav");
        //static SoundPlayer crushSound = new SoundPlayer("sound\\Crush.wav");

        public static int GameCounter = 0;

        public static void Start()
        {
            Score = 0;
            BackGround = (Bitmap)Image.FromFile("img\\Background.bmp");
            Hero = new Hero(PlayerSprite1, new Point(200, 500));
            gameObjects = new LinkedList<IGameObject>();
            bullets = new LinkedList<Bullet>();
            Timer.Start();
        }

        public static void AddBullet()
        {
            var bullet2 = new Bullet(new Point(Hero.Position.X - 15, Hero.Position.Y - 20));
            var bullet3 = new Bullet(new Point(Hero.Position.X + 15, Hero.Position.Y - 20));
            var bullet4 = new Bullet(new Point(Hero.Position.X - 8, Hero.Position.Y - 16));
            var bullet5 = new Bullet(new Point(Hero.Position.X + 8, Hero.Position.Y - 16));

            bullets.AddLast(bullet2);
            bullets.AddLast(bullet3);
            bullets.AddLast(bullet4);
            bullets.AddLast(bullet5);

            gameObjects.AddLast(bullet2);
            gameObjects.AddLast(bullet3);
            gameObjects.AddLast(bullet4);
            gameObjects.AddLast(bullet5);
        }

        public static void AddObject()
        {
            var xRandom = r.Next(0, 400);
            if (xRandom % 49 == 0)
                gameObjects.AddLast(new Healer(new Point(xRandom, -50)));
            else if (xRandom % 2 == 0)
                gameObjects.AddLast(new Commet(new Point(xRandom, -50)));
            else
                gameObjects.AddLast(new BigCommet(new Point(xRandom, -50)));
        }

        public static void Update()
        {
            var forCleaning = new List<IGameObject>();
            foreach (var gameObject in gameObjects)
            {
                gameObject.SetNewPosition();
                if (PositionCheck(gameObject))
                    forCleaning.Add(gameObject);
                if (IsBumpToHero(gameObject))
                {
                    forCleaning.Add(gameObject);
                    Hero.Heals -= gameObject.Damage;
                }

                if (!(gameObject is Bullet))
                {
                    foreach (var bullet in bullets)
                    {
                        if (IsShooted(bullet, gameObject))
                        {
                            gameObject.Heals -= bullet.Damage;
                            if (gameObject.Heals <= 0)
                            {
                                Score++;
                                forCleaning.Add(gameObject);
                            }

                            forCleaning.Add(bullet);
                        }
                    }
                }

                if (Hero.Heals <= 0)
                    GameOver();
            }

            foreach (var gameObject in forCleaning)
            {
                gameObjects.Remove(gameObject);
                if (gameObject is Bullet bullet)
                    bullets.Remove(bullet);
            }
        }

        public static IEnumerable<IGameObject> ReturnAllObjects()
        {
            var result = gameObjects.ToList();
            result.Add(Hero);
            return result;
        }

        private static bool PositionCheck(IGameObject gameObject)
        {
            return gameObject.Position.Y > 650 || gameObject is Bullet && gameObject.Position.Y < -10;
        }

        private static bool IsBumpToHero(IGameObject ob)
        {
            if (ob is Bullet) return false;
            var deltaX = Hero.Position.X - ob.Position.X;
            var deltaY = Hero.Position.Y - ob.Position.Y;
            var distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            return distance < ob.HitboxRadius + Hero.HitboxRadius;
        }

        private static bool IsShooted(Bullet bullet, IGameObject gameObject)
        {
            var deltaX = bullet.Position.X - gameObject.Position.X;
            var deltaY = bullet.Position.Y - gameObject.Position.Y;
            var distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            return distance < gameObject.HitboxRadius + 1;
        }

        private static void GameOver()
        {
            Timer.Stop();
            gameObjects = new LinkedList<IGameObject>();
            SetNewHeightScore();
            Hero = new Hero((Bitmap)Image.FromFile("img\\GameOver.png"), new Point(200, 300));
        }

        private static void SetNewHeightScore()
        {
            var scores = LoadOldHeightScore();
            var newScore = Score;
            var builder = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                if (scores[i] < newScore)
                {
                    var tmp = scores[i];
                    scores[i] = newScore;
                    newScore = tmp;
                }

                builder.Append(scores[i]);
                builder.Append('\n');
            }

            File.WriteAllText("heightScores.txt", builder.ToString());
        }

        public static List<int> LoadOldHeightScore()
        {
            var lines = File.ReadAllLines("heightScores.txt");
            return lines.Select(int.Parse).ToList();
        }
    }
}