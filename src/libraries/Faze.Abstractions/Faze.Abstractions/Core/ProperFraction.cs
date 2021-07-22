using System;

namespace Faze.Abstractions.Core
{
    /// <summary>
    /// Represents a number greater or equal to 0 and less than 1
    /// </summary>
    public struct ProperFraction
    {
        private readonly double value;

        public ProperFraction(double value)
        {
            if (value < 0 || value >= 1)
                throw new ArgumentOutOfRangeException(nameof(value), $"A {nameof(ProperFraction)} must have a value greater or equal to 0 and less than 1");

            this.value = value;
        }

        public static implicit operator double(ProperFraction properFraction)
        {
            return properFraction.value;
        }

        public static implicit operator ProperFraction(double value)
        {
            return new ProperFraction(value);
        }

        public override bool Equals(object obj)
        {
            if (obj is ProperFraction ui)
            {
                return value.Equals(ui.value);
            }

            return value.Equals(obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
