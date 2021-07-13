using System;

namespace Faze.Abstractions.Core
{
    /// <summary>
    /// Represents a number between 0 and 1 inclusive
    /// </summary>
    public struct UnitInterval
    {
        private readonly double value;

        public UnitInterval(double value)
        {
            if (value < 0 || value > 1)
                throw new ArgumentOutOfRangeException(nameof(value), $"A UnitInterval must have a value between 0 - 1");

            this.value = value;
        }

        public static implicit operator double(UnitInterval unitInterval)
        {
            return unitInterval.value;
        }

        public static implicit operator UnitInterval(double value)
        {
            return new UnitInterval(value);
        }
    }
}
