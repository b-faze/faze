namespace Faze.Rendering.Video.FFMPEG.Extensions
{
    public class VideoFFMPEGSettings
    {
        public VideoFFMPEGSettings(int fps)
        {
            Fps = fps;
            ImageVCodec = FFMPEGVCodec.Png;
        }

        /// <summary>
        /// Frames per second
        /// </summary>
        public int Fps { get; }
        public FFMPEGVCodec ImageVCodec { get; set; }

        internal static string GetVCodecArgument(FFMPEGVCodec vcodec)
        {
            return vcodec.ToString().ToLower();
        }
    }
}
