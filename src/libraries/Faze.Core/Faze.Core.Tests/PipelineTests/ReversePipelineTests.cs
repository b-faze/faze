using Faze.Core.Pipelines;
using Faze.Utilities.Testing;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Faze.Core.Tests.PipelineTests
{
    public class ReversePipelineTests
    {
        [Fact]
        public void CanCreateEmpty()
        {
            var builder = ReversePipelineBuilder.Create();
        }

        [Fact]
        public void CanCreateAndRunPipelineWithInput()
        {
            var expectedValue = 55;
            var expectedMessage = "Test";

            var pipeline = ReversePipelineBuilder.Create()
                .Require<int>(i =>
                {
                    i.ShouldBe(expectedValue);
                    throw new Exception(expectedMessage);
                })
                .Build();

            Should.Throw<Exception>(() =>
            {
                pipeline.Run(expectedValue);
            });
        }

        [Fact]
        public void CanCreateAndRunPipelineWithoutInput()
        {
            var expectedValue = 99;
            var expectedMessage = "Test";

            var pipeline = ReversePipelineBuilder.Create()
                .Require<int>(i =>
                {
                    i.ShouldBe(expectedValue);
                    throw new Exception(expectedMessage);
                })
                .Build(() => expectedValue);

            Should.Throw<Exception>(() =>
            {
                pipeline.Run();
            });
        }

        [Fact]
        public void CanCreateAndRunPipelineWithProgress()
        {
            var progress = new TestProgressTracker();

            var pipeline = ReversePipelineBuilder.Create()
                .Require<int>((i, progress) =>
                {
                    var testProgress = (TestProgressTracker)progress;
                    testProgress.MaxTicks.ShouldBe(0);
                    testProgress.Ticks.ShouldBe(0);
                    progress.Tick();
                    testProgress.Ticks.ShouldBe(1);
                })
                .Build(() => 0);

            pipeline.Run(progress);

            progress.MaxTicks.ShouldBe(2);
            progress.Ticks.ShouldBe(2);
        }
    }
}
