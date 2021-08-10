using Faze.Core.Pipelines;
using System;
using Xunit;
using Faze.Rendering.Video.Extensions;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xabe.FFmpeg;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using Faze.Core.Extensions;
using Faze.Rendering.TreeRenderers;
using Faze.Rendering.TreePainters;
using Faze.Core.Data;

namespace Faze.Rendering.Video.Tests
{
    public class VideoPipelineTests
    {
        [Fact]
        public void CanPipeFramesToFFMPEG()
        {
            var pipeline = ReversePipelineBuilder.Create()
                .Video("test.mp4", new VideoFFMPEGSettings(24))
                .StreamStreamer()
                .Merge()
                .Require<string>(dir =>
                {
                    return Directory.GetFiles(dir).Where(x => x.EndsWith(".jpeg")).Select(f => File.OpenRead(f));
                })
                .Build();

            pipeline.Run(@"G:\test_images");
        }

        [Fact]
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
                .MergeStreamers()
                .Map(builder => builder
                    .StreamRender()
                    .Render(renderer)
                )
                .Iterate(100, () =>
                {
                    rendererOptions.BorderProportion += 0.002f;
                })
                .Paint<object>(new CheckeredTreePainter())
                .LoadTree(new DynamicSquareTreeOptions<object>(treeSize, treeDepth, info => null), new DynamicTreeDataProvider<object>());

            pipeline.Run();
        }
    }
}
