namespace Faze.Examples.Gallery.Interfaces
{
    public class ImageGeneratorMetaData
    {
        public ImageGeneratorMetaData(string[] albums) 
        {
            Albums = albums;
        }

        public string[] Albums { get; }
    }
}
