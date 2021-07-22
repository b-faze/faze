namespace Faze.Abstractions.Core
{
    /// <summary>
    /// Reads and writes a value of type <typeparamref name="T"/> as a string
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValueSerialiser<T>
    {
        string Serialize(T value);
        T Deserialize(string valueString);
    }
}
