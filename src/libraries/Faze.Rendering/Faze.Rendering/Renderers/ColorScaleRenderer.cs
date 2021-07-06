using Faze.Abstractions.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Faze.Rendering.Renderers
{
    public class ColorScaleRenderer
    {
        public Bitmap Draw(IColorInterpolator colorInterpolator, int width, int height)
        {
            var img = new Bitmap(width, height);
            for (var i = 0; i < width; i++)
            {
                var fraction = (double)i / width;
                var color = colorInterpolator.GetColor(fraction);

                for (var j = 0; j < height; j++) 
                {
                    img.SetPixel(i, j, color);
                }

            }

            return img;
        }
    }
}
