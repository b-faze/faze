using Faze.Abstractions.Core;
using Faze.Core.Streamers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Faze.Rendering.Video.FFMPEG.Extensions
{
    public static class ReversePipelineBuilderExtensions
    {
        /// <summary>
        /// Pipes a stream of JPEGs to ffmpeg
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="filename"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static IReversePipelineBuilder<IStreamer> Video(this IReversePipelineBuilder builder, string filename, VideoFFMPEGSettings settings)
        {
            return builder.Require<IStreamer>(streamer =>
            {
                var fps = settings.Fps;
                var imgVCodec = VideoFFMPEGSettings.GetVCodecArgument(settings.ImageVCodec);
                var startInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = $"-y -f image2pipe -vcodec {imgVCodec} -r {fps} -i - -codec:v mpeg4 -q:v 2 -r {fps} \"{filename}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    streamer.WriteToStream(process.StandardInput.BaseStream);

                    process.StandardInput.Flush();
                    process.StandardInput.Close();

                    var error = process.StandardError.ReadToEnd();
                    var output = process.StandardOutput.ReadToEnd();

                    process.WaitForExit();

                    if (process.ExitCode == 1)
                    {
                        throw new Exception(error);
                    }
                }
            });
        }
    }
}
