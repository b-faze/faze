using Faze.Core.Pipelines;
using System;
using Xunit;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Faze.Core.Extensions;
using Faze.Rendering.TreeRenderers;
using Faze.Rendering.TreePainters;
using Faze.Core.Data;
using Faze.Rendering.Video.FFMPEG.Extensions;

namespace Faze.Rendering.Video.FFMPEG.Tests
{
    public class VideoPipelineTests
    {
        [Fact(Skip = "Manual")]
        public void CanPipeFramesToFFMPEG()
        {
            var pipeline = ReversePipelineBuilder.Create()
                .Video("test.mp4", new VideoFFMPEGSettings(24)
                {
                    ImageVCodec = FFMPEGVCodec.Mjpeg
                })
                .StreamStreamer()
                .Merge()
                .Require<string>(dir =>
                {
                    return Directory.GetFiles(dir).Where(x => x.EndsWith(".jpeg")).Select(f => File.OpenRead(f));
                })
                .Build();

            pipeline.Run(@"G:\test_images");
        }

        [Fact(Skip = "Manual")]
        public void CanCreateIterativePipeline()
        {
            var treeSize = 3;
            var treeDepth = 3;
            var imageSize = 500;

            var rendererOptions = new SquareTreeRendererOptions(treeSize, imageSize)
            {
                BorderProportion = 0f
            };
            var renderer = new SquareTreeRenderer(rendererOptions);

            var pipeline = ReversePipelineBuilder.Create()
                .Video("video-border.mp4", new VideoFFMPEGSettings(24))
                .Merge()
                .Map(builder => builder
                    .Render(renderer)
                )
                .Iterate(100, _ =>
                {
                    rendererOptions.BorderProportion += 0.002f;
                })
                .Paint<object>(new CheckeredTreePainter())
                .LoadTree(new DynamicSquareTreeOptions<object>(treeSize, treeDepth, info => null), new DynamicTreeDataProvider<object>());

            pipeline.Run();
        }
    }
}
