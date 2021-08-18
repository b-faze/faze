using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Faze.Examples.Gallery.Extensions
{
    public static class BitmapExtensions
    {
        public static Bitmap SetMetaValue(this Bitmap sourceBitmap, MetaProperty property, string value) {
            return SetMetaValue(sourceBitmap, (int)property, value);
        }
        public static Bitmap SetMetaValue(this Bitmap sourceBitmap, int property, string value)
        {
            PropertyItem prop = sourceBitmap.PropertyItems[0];
            int iLen = value.Length + 1;
            byte[] bTxt = new byte[iLen];
            for (int i = 0; i < iLen - 1; i++)
                bTxt[i] = (byte)value[i];
            bTxt[iLen - 1] = 0x00;
            prop.Id = (int)property;
            prop.Type = 2;
            prop.Value = bTxt;
            prop.Len = iLen;
            sourceBitmap.SetPropertyItem(prop);
            return sourceBitmap;
        }

        public static string GetMetaValue(this Bitmap sourceBitmap, MetaProperty property)
        {
            PropertyItem[] propItems = sourceBitmap.PropertyItems;
            var prop = propItems.FirstOrDefault(p => p.Id == (int)property);
            if (prop != null)
            {
                return Encoding.UTF8.GetString(prop.Value);
            }
            else
            {
                return null;
            }
        }

    }

    public enum MetaProperty
    {
        TagTitle = 0x0320,
        TagDesc = 0x010E,
        Title = 40091,
        Comment = 40092,
        Author = 40093,
        Keywords = 0x010E,
        Subject = 40095,
        Copyright = 33432,
        Software = 0x0131,
        DateTime = 36867
    }
}
