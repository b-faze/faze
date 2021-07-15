using Faze.Abstractions.Core;

namespace Faze.Core.Serialisers
{
    public class NullableIntSerialiser : IValueSerialiser<int?>
    {
        public int? Deserialize(string valueString)
        {
            return int.TryParse(valueString, out var value) ? value : (int?)null;
        }

        public string Serialize(int? value)
        {
            return value?.ToString() ?? "null";
        }
    }
}
