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

namespace Faze.Rendering.Video.Tests
{
    public class VideoPipelineTests
    {
        [Fact]
        public void CanPipeFramesToFFMPEG()
        {
            var pipeline = ReversePipelineBuilder.Create()
                .Video("test.mp4", new VideoFFMPEGSettings(24))
                .Merge()
                .Require<string>(dir =>
                {
                    return Directory.GetFiles(dir).Where(x => x.EndsWith(".jpeg")).Select(f => File.OpenRead(f));
                })
                .Build();

            pipeline.Run(@"G:\test_images");
        }
    }
}
