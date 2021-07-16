# IPaintedTreeRenderer

A pained tree renderer is responsible for producing images given a 'painted tree'. A painted tree is a tree that has a colour value at each node. If the renderer supports viewports, it is also possible for the renderer to trim a given tree to only its visible nodes - see [Real-time Rendering](../../concept/real-time-rendering.md) for more information.

```csharp
    public interface IPaintedTreeRenderer
    {
        Tree<T> GetVisible<T>(Tree<T> tree);
        void Draw(Tree<Color> tree);
        void Save(Stream stream);
    }
```

## Members

### GetVisible

```csharp
Tree<T> GetVisible<T>(Tree<T> tree);
```

Returns a new tree which excludes any nodes/branches that will not be rendered according to the current viewport or another other setting. This would also include pruning branches past a certain depth if drawing them would result in sub-pixel rendering.

### Draw

```csharp
void Draw(Tree<Color> tree);
```

Draws the given tree to the renderer's internal state

### Save

```csharp
void Save(Stream stream);
```

Saves the renderer's current state to the given stream.

