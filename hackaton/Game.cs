using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace hackaton
{
    public static class Game
    {
        public static Bitmap BackGround;
        public static Hero hero;
        public static LinkedList<IGameObject> gameObjects;
        public static int GameCounter = 0; //TODO: дописать добавление обжэкта по каунтеру

        public static void Start()
        {
            BackGround = (Bitmap) Image.FromFile("img\\Background.bmp");
            hero = new Hero((Bitmap) Image.FromFile("img\\Hero.png"), new Point(200, 500));
            gameObjects = new LinkedList<IGameObject>();
        }

        public static void AddObject()
        {
            var r = new Random();
            var xRandom = r.Next(0, 400);
            switch (r.Next(0, 100) % 3)
            {
                case 0:
                    gameObjects.AddLast(new Commet((Bitmap) Image.FromFile("img\\Commet.png"),
                        new Point(50, -150)));
                    break;
                case 1:
                    gameObjects.AddLast(new BigCommet((Bitmap) Image.FromFile("img\\Commet.png"),
                        new Point(150, -150)));
                    break;
                case 2:
                    gameObjects.AddLast(new Planet((Bitmap) Image.FromFile("img\\Commet.png"),
                        new Point(200, -150)));
                    break;
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
            }

            foreach (var gameObject in forClering)
            {
                gameObjects.Remove(gameObject);
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
    }
}
