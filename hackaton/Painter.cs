using System;
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
        public Painter(int width, int heigth)
        {
            this.width = width;
            this.heigth = heigth;
        }

        public Bitmap Paint(IEnumerable<IGameObject> source)
        {
            return null;
        }
    }
}
