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
        public static Bitmap CommetSprite = (Bitmap) Image.FromFile("img\\Comet.png");
        public static Bitmap BigCommetSprite = (Bitmap)Image.FromFile("img\\BigComet.png");
        public static Bitmap BrokenBigCommetSprite = (Bitmap)Image.FromFile("img\\BrokenBigComet.png");
        public static Bitmap BulletSprite = (Bitmap) Image.FromFile("img\\Bullet.png");
        public static Bitmap PlayerSprite1 = (Bitmap) Image.FromFile("img\\Player1.png");
        public static Bitmap PlayerSprite2 = (Bitmap)Image.FromFile("img\\Player2.png");
        public static Bitmap PlayerSprite3 = (Bitmap)Image.FromFile("img\\Player3.png");
        public static Bitmap HealerSprite = (Bitmap)Image.FromFile("img\\healUp.png");
        public static Bitmap BackGround;
        public static Hero hero;
        public static LinkedList<IGameObject> gameObjects;
        public static LinkedList<Bullet> Bullets;
        public static int Score;
        private static Random r = new Random();
        static SoundPlayer hit = new SoundPlayer("Sound.wav");
        public static Timer timer;
        static SoundPlayer hitSound = new SoundPlayer("sound\\Hit.wav");
        static SoundPlayer crushSound = new SoundPlayer("sound\\Crush.wav");
        public static SoundPlayer music = new SoundPlayer("sound\\Music.wav");

        public static int GameCounter = 0;

        public static void Start()
        {
            Score = 0;
            BackGround = (Bitmap) Image.FromFile("img\\Background.bmp");
            hero = new Hero(PlayerSprite1, new Point(200, 500));
            gameObjects = new LinkedList<IGameObject>();
            Bullets = new LinkedList<Bullet>();
            timer.Start();
        }
        public static void AddBullet()
        {
            
            var bullet2 = new Bullet(new Point(hero.Position.X - 15, hero.Position.Y - 20));
            var bullet3 = new Bullet(new Point(hero.Position.X + 15, hero.Position.Y - 20));
            var bullet4 = new Bullet(new Point(hero.Position.X - 8, hero.Position.Y - 16));
            var bullet5 = new Bullet(new Point(hero.Position.X + 8, hero.Position.Y - 16));
            
            Bullets.AddLast(bullet2);
            Bullets.AddLast(bullet3);
            Bullets.AddLast(bullet4);
            Bullets.AddLast(bullet5);
            
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
                if (hero.Heals <= 0)
                    GameOver();
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
            timer.Stop();
            gameObjects = new LinkedList<IGameObject>();
            SetNewHeightScore();
            hero = new Hero((Bitmap)Image.FromFile("img\\GameOver.png"), new Point(200, 300));
        }

        public static void SetNewHeightScore()
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
