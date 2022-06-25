namespace Faze.Examples.Gallery.Visualisations.Rubik
{
    public class RubikPainterConfig
    {
        public bool Normalise { get; set; }
        public RubikUnavailablePaintType UnavailablePaintType { get; set; }
    }

    public enum RubikUnavailablePaintType
    {
        Ignore,
        Black,
        Average
    }
}
