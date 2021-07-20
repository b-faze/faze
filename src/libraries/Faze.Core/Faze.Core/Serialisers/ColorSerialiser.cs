using Faze.Abstractions.Core;
using System.Drawing;
using System.Linq;

namespace Faze.Core.Serialisers
{
    public class ColorSerialiser : IValueSerialiser<Color>
    {
        public Color Deserialize(string valueString)
        {
            var parts = valueString.Split(',').Select(int.Parse).ToArray();
            
            return Color.FromArgb(parts[0], parts[1], parts[2], parts[3]);
        }

        public string Serialize(Color value)
        {
            return string.Join(",", new[] { value.A, value.R, value.G, value.B });
        }
    }
}
