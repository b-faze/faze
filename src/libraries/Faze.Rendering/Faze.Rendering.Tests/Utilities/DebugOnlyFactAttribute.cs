using System.Diagnostics;
using Xunit;

namespace Faze.Rendering.Tests.Utilities
{
    public class DebugOnlyFactAttribute : FactAttribute
    {
        public DebugOnlyFactAttribute()
        {
            if (!Debugger.IsAttached)
            {
                Skip = "Only running in interactive mode.";
            }
        }
    }
}
