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
        private Bitmap Result { get; set; }
        private int Width { get; }
        private int Height { get; }
        private Bitmap Background { get; }

        public Painter(PictureBox pictureBox, Bitmap background)
        {
            Width = pictureBox.Width;
            Height = pictureBox.Height;
            Background = background;
        }

        public Bitmap Paint(IEnumerable<IGameObject> objects)
        {
            Result = new Bitmap(Background);
            foreach (var obj in objects.OrderBy(x => x.Priority))
            {
                var location = PositionTransformer(obj);
                for (var i = 0; i < obj.Sprite.Width; i++)
                for (var j = 0; j < obj.Sprite.Height; j++)
                {
                    var x = location.X + i;
                    var y = location.Y + j;
                    
                    if (x > 0 & y > 0 & x < Width & y < Height)
                    {
                        var newPixel = obj.Sprite.GetPixel(i, j);
                        var oldPixel = Result.GetPixel(x, y);
                        var resR = (newPixel.A * newPixel.R + oldPixel.R * (255 - newPixel.A)) / 255;
                        var resG = (newPixel.A * newPixel.G + oldPixel.G * (255 - newPixel.A)) / 255;
                        var resB = (newPixel.A * newPixel.B + oldPixel.B * (255 - newPixel.A)) / 255;
                        var resultPixel = Color.FromArgb(255, resR, resG, resB);
                        Result.SetPixel(x, y, resultPixel);
                    }
                }
            }

            return Result;
        }

        private static Point PositionTransformer(IGameObject obj)
        {
            return new Point(
                obj.Position.X - obj.Sprite.Width / 2,
                obj.Position.Y - obj.Sprite.Height / 2);
        }
    }
}