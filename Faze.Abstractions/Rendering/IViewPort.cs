namespace Faze.Abstractions.Rendering
{
    public interface IViewPort
    {
        float Left { get; }
        float Top { get; }
        float Width { get; }
        float Height { get; }
        float Scale { get; }
    }
}
