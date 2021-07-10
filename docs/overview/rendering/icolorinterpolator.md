# IColorInterpolator

Colour interpolators implement the `IColorInterpolator`  interface

```text
    public interface IColorInterpolator
    {
        Color GetColor(double d);
    }
```

Given a number between 0 - 1, colour interpolators will map it to a colour.

## Examples

For more examples, and the complete list of available colour interpolators, see [Rendering.ColorInterpolators](../../rendering/color-interpolators.md)

### Greyscale

The GreyscaleColorInterpolator is a common linear interpolator from Black -&gt; While

![DrawGreyscale](https://raw.githubusercontent.com/b-faze/Faze.Rendering/master/Documentation/Wiki/Images/DrawGreyscale.png)

### Linear

A generalised linear interpolator where you specify the two colours to interpolate between.

![DrawLinearBlueRed](https://raw.githubusercontent.com/b-faze/Faze.Rendering/master/Documentation/Wiki/Images/DrawLinearBlueRed.png)

### Gold

A custom interpolator which emphasises the mid-range, going from Blue -&gt; Yellow -&gt; Red

![DrawGold](https://raw.githubusercontent.com/b-faze/Faze.Rendering/master/Documentation/Wiki/Images/DrawGold.png)

