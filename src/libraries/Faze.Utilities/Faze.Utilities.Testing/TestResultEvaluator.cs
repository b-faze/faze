using Faze.Engine.Players;

namespace Faze.Utilities.Testing
{
    public class TestResultEvaluator : IResultEvaluator<int?>
    {
        public int? MinValue => int.MinValue;

        public int? MaxValue => int.MaxValue;

        public int Compare(int? a, int? b)
        {
            if (a == b)
                return 0;

            if (a != null && b != null)
                return a.Value.CompareTo(b);

            if (a != null)
                return 1; // a > null

            return -1; // null < b

        }
    }
}
