using Faze.Abstractions.Core;
using Shouldly;
using Xunit;

namespace Faze.Abstractions.Tests
{
    public class NullProgressTrackerTests
    {
        private IProgressTracker progressTracker;

        public NullProgressTrackerTests()
        {
            this.progressTracker = NullProgressTracker.Instance;
        }

        [Fact]
        public void TickDoesNothing()
        {
            progressTracker.Tick();
        }

        [Fact]
        public void SettersDoNothing()
        {
            progressTracker.SetMaxTicks(5);
            progressTracker.SetMessage("message");
        }

        [Fact]
        public void SpawnReturnsTheSameObject()
        {
            progressTracker.Spawn().ShouldBe(progressTracker);
        }

    }

}
