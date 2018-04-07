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

        public static void Start()
        {
           BackGround = (Bitmap) Image.FromFile("img\\Background.bmp");
            hero = new Hero((Bitmap) Image.FromFile("img\\Hero.png"), new Point(200, 530), 50);
            gameObjects = new LinkedList<IGameObject>();
        }

        public static void AddObject<T>()
        {
            throw new NotImplementedException();
        }

        public static void Update()
        {

            foreach (var gameObject in gameObjects)
            {
                gameObject.SetNewPosition();
                PositionCheck(gameObject);
            }
        }

        public static IEnumerable<IGameObject> ReturnAllObjects()
        {
            var result = gameObjects.ToList();
            result.Add(hero);
            return result;
        }

        private static void PositionCheck(IGameObject gameObject)
        {
            if (gameObject.Position.Y > 650) gameObjects.Remove(gameObject);
        }        
    }
}
