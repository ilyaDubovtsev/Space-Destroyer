using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace hackaton
{
    public class Painter
    {
        Bitmap result;
        int width;
        int heigth;
        Bitmap background;
        public Painter(PictureBox pictureBox, Bitmap background)
        {
            width = pictureBox.Width;
            heigth = pictureBox.Height;
            this.background = background;
        }

        public Bitmap Paint(IEnumerable<IGameObject> objects, Hero hero)
        {
            result = new Bitmap(background);
            foreach (var obj in objects.OrderBy(x => x.Priority))
            {
                var location = PositionTransformer(obj);
                for (int i = 0; i < obj.Sprite.Width; i++)
                    for (int j = 0; j < obj.Sprite.Height; j++)
                    {
                        var x = location.X + i;
                        var y = location.Y + j;
                        if (x > 0 & y > 0 & x < width & y < heigth)
                        {
                            var newPixel = obj.Sprite.GetPixel(i, j);
                            var oldPixel = result.GetPixel(x, y);
                            int resR = (newPixel.A*newPixel.R + oldPixel.R * (255 - newPixel.A))/255;
                            int resG = (newPixel.A * newPixel.G + oldPixel.G * (255 - newPixel.A)) / 255;
                            int resB = (newPixel.A * newPixel.B + oldPixel.B * (255 - newPixel.A)) / 255;
                            var resultPixel = Color.FromArgb(255, resR, resG, resB);
                                result.SetPixel(x, y, resultPixel);
                        }
                    }
            }
            var loc = PositionTransformer(hero);
            for (int i = 0; i < hero.Sprite.Width; i++)
                for (int j = 0; j < hero.Sprite.Height; j++)
                {
                    var x = loc.X + i;
                    var y = loc.Y + j;
                    if (x > 0 & y > 0 & x < width & y < heigth)
                    {
                        var newPixel = hero.Sprite.GetPixel(i, j);
                        var oldPixel = result.GetPixel(x, y);
                        int resR = (int)(newPixel.A * newPixel.R + oldPixel.R * (255 - newPixel.A)) / 255;
                        int resG = (int)(newPixel.A * newPixel.G + oldPixel.G * (255 - newPixel.A)) / 255;
                        int resB = (int)(newPixel.A * newPixel.B + oldPixel.B * (255 - newPixel.A)) / 255;
                        var resultPixel = Color.FromArgb(255, resR, resG, resB);
                        result.SetPixel(x, y, resultPixel);
                    }
                }
            return result;
        }

        private Point PositionTransformer(IGameObject obj)
        {
            return new Point(
                obj.Position.X - obj.Sprite.Width / 2,
                obj.Position.Y - obj.Sprite.Height / 2);
        }
    }
}
