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
        static LinkedList<IGameObject> gameObjects;

        public static void Start()
        {
            //Bitmap = new Bitmap();
            //hero = new Hero(Bitmap.FromFile(), new Point(200, 500), 50);
        }

        public static void Update()
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.SetNewPosition();
                PositionCheck(gameObject);
            }


        }

        private static bool PositionCheck(IGameObject gemeObject)
        {
            throw new NotImplementedException();
        }

    }
}
