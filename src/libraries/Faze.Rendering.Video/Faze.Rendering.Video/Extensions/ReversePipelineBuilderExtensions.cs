using Faze.Abstractions.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Faze.Rendering.Video.Extensions
{
    public static class ReversePipelineBuilderExtensions
    {
        public static IReversePipelineBuilder<Stream> StreamStreamer(this IReversePipelineBuilder<IStreamer> builder)
        {
            return builder.Require<Stream>(stream => new StreamStreamer(stream));
        }

        public static IReversePipelineBuilder<IEnumerable<Stream>> Merge(this IReversePipelineBuilder<Stream> builder)
        {
            return builder.Require<IEnumerable<Stream>>(streams => new ConcatenatedStream(streams));
        }

        public static IReversePipelineBuilder<IEnumerable<IStreamer>> MergeStreamers(this IReversePipelineBuilder<IStreamer> builder)
        {
            return builder.Require<IEnumerable<IStreamer>>(streamers => new EnumerableStreamer(streamers));
        }

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
                var startInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = $"-y -f image2pipe -vcodec mjpeg -r {fps} -i - -vcodec mpeg4 -r {fps} \"{filename}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    try
                    {
                        streamer.WriteToStream(process.StandardInput.BaseStream);
                    }
                    catch (Exception ex)
                    {

                    }

                    //using (frameStream)
                    //{         
                    //    // TODO read with buffer to improve performance
                    //    int b;
                    //    while ((b = frameStream.ReadByte()) > -1)
                    //    {
                    //        process.StandardInput.BaseStream.WriteByte((byte)b);
                    //    }
                    //}

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
