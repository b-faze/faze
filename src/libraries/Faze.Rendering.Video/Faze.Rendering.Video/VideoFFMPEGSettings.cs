namespace Faze.Rendering.Video.Extensions
{
    public class VideoFFMPEGSettings
    {
        public VideoFFMPEGSettings(int fps)
        {
            Fps = fps;
            ImageVCodec = FFMPEGVCodec.Png;
        }

        public int Fps { get; }
        public FFMPEGVCodec ImageVCodec { get; set; }

        internal static string GetVCodecArgument(FFMPEGVCodec vcodec)
        {
            return vcodec.ToString().ToLower();
        }
    }
}
