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
        private static Hero hero;
        static LinkedList<IGameObject> GameObjects;

        public static void Start()
        {
            BackGround = (Bitmap) Image.FromFile("\\assets\\img\\Hero.png");
            hero = new Hero((Bitmap) Image.FromFile("\\assets\\img\\Background.bmp"), new Point(200, 500), 50);
        }

        public static void Update()
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject.SetNewPosition();
                PositionCheck(gameObject);
            }
        }

        private static void PositionCheck(IGameObject gameObject)
        {
            if (gameObject.Position.Y > 650) GameObjects.Remove(gameObject);
        }

    }
}
