using System.Diagnostics;
using Xunit;

namespace Faze.Rendering.Tests.Utilities
{
    public class DebugOnlyTheoryAttribute : TheoryAttribute
    {
        public DebugOnlyTheoryAttribute()
        {
            if (!Debugger.IsAttached)
            {
                Skip = "Only running in interactive mode.";
            }
        }
    }
}
