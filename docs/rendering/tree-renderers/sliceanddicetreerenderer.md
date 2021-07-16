# SliceAndDiceTreeRenderer

Unlike SquareTreeRenderer, this renderer can handle arbitrary trees which can have varying branches at each depth. The renderer works by splitting the image into equal rectangles equal to the number of branches at each depth. It works in the same way as [d3's treemap sliceanddice](https://github.com/d3/d3-hierarchy#treemapSliceDice).

### Options

`SliceAndDiceTreeRendererOptions`

```csharp
    public double BorderProportion { get; set; }
    public IViewport Viewport { get; set; }
    public int? MaxDepth { get; set; }
```

| Properties | Type | Description |
| :--- | :--- | :--- |
| BorderProportion | float | Border size at each depth as a fraction of the parent's size |
| Viewport | IViewport | Renders only a sub-portion of the image. Used for zooming |
| MaxDepth | int? | If a value is set, the renderer will ignore nodes past the specified depth |

### Examples

To do

