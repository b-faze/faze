namespace Faze.Rendering.Video.Extensions
{
    public class VideoFFMPEGSettings
    {
        public VideoFFMPEGSettings(int fps)
        {
            Fps = fps;
        }

        public int Fps { get; }
    }
}
