using Faze.Abstractions.Core;

namespace Faze.Utilities.Testing
{
    public class TestNullObjectSerialiser : IValueSerialiser<object>
    {
        public object Deserialize(string valueString)
        {
            return null;
        }

        public string Serialize(object value)
        {
            return null;
        }
    }
}
