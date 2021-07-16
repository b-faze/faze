# ITreePainter

Either maps a tree of generic type T or a known type to a 'painted tree'. The result can then be passed into an IPaintedTreeRenderer. 

```csharp
    public interface ITreePainter
    {
        Tree<Color> Paint<T>(Tree<T> tree);
    }

    public interface ITreePainter<TValue>
    {
        Tree<Color> Paint(Tree<TValue> tree);
    }
```

{% hint style="info" %}
It is not necessary to implement these interfaces, however it does allow for clearer code and for special pipeline extensions to be used.
{% endhint %}

