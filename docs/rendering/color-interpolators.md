# Color Interpolators

## Linear

The `LinearColorInterpolator` is a generalised linear interpolator where you specify the two colours to interpolate between.

![](../.gitbook/assets/drawlinearbluered.png)

### Options

| Property | Type | Description |
| :--- | :--- | :--- |
| Start | Color | Color if value is 0 |
| End | Color | Color if value is 1 |

## Greyscale

The `GreyscaleColorInterpolator` is a common linear interpolator from Black -&gt; While

![](../.gitbook/assets/drawgreyscale.png)

```csharp
public class GreyscaleInterpolator : LinearColorInterpolator, IColorInterpolator
```

### Options

| Property | Type | Description |
| :--- | :--- | :--- |
| Reverse | bool | If true, starts from white instead of black |

## Gold

A custom interpolator which emphasises the mid-range, going from Blue -&gt; Yellow -&gt; Red

![](../.gitbook/assets/drawgold.png)

### Channels

| Channel | Formula | Graph |
| :--- | :--- | :--- |
| Red | $$255x + 127(1 - 2|\frac{1}{2} - x|)$$  | ![](../.gitbook/assets/gold_red2.png)  |
| Green | $$127(1 - 2|\frac{1}{2} - x|)$$  | ![](../.gitbook/assets/gold_blue-2-.png)  |
| Blue | $$255(1 - x) - 127(1 - 2|\frac{1}{2}-x|)$$  |  ![](../.gitbook/assets/gold_blue-2-.png) |

### Options

_None_

