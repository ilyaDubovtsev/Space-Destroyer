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
        int width;
        int heigth;
        Bitmap background;
        public Painter(PictureBox pictureBox, Bitmap background)
        {
            width = pictureBox.Width;
            heigth = pictureBox.Height;
            this.background = background;
        }

        public Bitmap Paint(IEnumerable<IGameObject> source)
        {
            Bitmap result = new Bitmap(background);
            foreach (var obj in source.OrderBy(x => x.Priority))
            {
                var location = PositionTransformer(obj);
                for (int i = 0; i < obj.Sprite.Width; i++)
                    for (int j = 0; j < obj.Sprite.Height; j++)
                    {
                        var x = location.X + i;
                        var y = location.Y + j;
                        if (x > 0 & y > 0 & x < width & y < heigth)
                            result.SetPixel(x, y, obj.Sprite.GetPixel(i, j));
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
