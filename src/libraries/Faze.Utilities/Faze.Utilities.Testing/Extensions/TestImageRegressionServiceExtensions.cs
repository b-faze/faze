using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Utilities.Testing.Extensions
{
    public static class TestImageRegressionServiceExtensions
    {
        public static IReversePipelineBuilder<IStreamer> TestImageDiffPipeline(this TestImageRegressionService service, string expectedImageId, string diffId = null)
        {
            return ReversePipelineBuilder.Create()
                .Require<IStreamer>(renderer => service.Compare(renderer, expectedImageId, diffId));
        }

        public static IReversePipelineBuilder<IStreamer> GenerateTestCasePipeline(this TestImageRegressionService service, string expectedImageId)
        {
            return ReversePipelineBuilder.Create()
                .Require<IStreamer>(renderer => service.GenerateTestCase(renderer, expectedImageId));
        }
    }
}
