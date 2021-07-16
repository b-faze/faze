# IPipeline

A pipeline represents a pre-built set of functions that are chained together. Its usage is very simple, either the pipeline has everything it needs to run i.e IPipeline or requires some initial input IPipeline&lt;TInput&gt;.

```csharp
public interface IPipeline
{
    void Run(IProgressTracker progress = null);
}

public interface IPipeline<TInput>
{
    void Run(TInput input, IProgressTracker progress = null);
}
```

An IPipeline is created from using a PipelineBuilder

## IReversePipelineBuilder

An IReversePipelineBuilder is used to build up a pipeline starting from the desired output, working backwards specifying the required input. It is useful for when you know what you want, but are still working out how to get there. There are useful extension methods that can help with working out how to get to the desired output:

* [Faze.Gallery.Extensions.PipelineExtensions](https://github.com/b-faze/faze/blob/d67c933395713416cb0109a63fa417cc832f10ba/src/examples/gallery/Faze.Examples.Gallery/Extensions/ReversePipelineExtensions.cs)
* [Faze.Core.Pipelines.PipelineExtensions](https://github.com/b-faze/faze/blob/d67c933395713416cb0109a63fa417cc832f10ba/src/libraries/Faze.Core/Faze.Core/Extensions/ReversePipelineExtensions.cs)

```csharp
public interface IReversePipelineBuilder
{
    IReversePipelineBuilder<TNext> Require<TNext>(Action<TNext> fn);
}

public interface IReversePipelineBuilder<T>
{
    IReversePipelineBuilder<TNext> Require<TNext>(Func<TNext, T> fn);
    IReversePipelineBuilder<TNext> Require<TNext>(Func<TNext, IProgressTracker, T> fn);
    IPipeline<T> Build();
    IPipeline Build(Func<T> fn);
}
```

### Example Usage

See [OXGoldImagePipeline.cs](https://github.com/b-faze/faze/blob/207ab4de2be5bd3a1317280c5c7826ac0aec2d29/src/examples/gallery/Faze.Examples.Gallery/Visualisations/OX/OXGoldImagePipeline.cs) for the example below.

```csharp
IPipeline pipeline = ReversePipelineBuilder.Create()
    .GallerySave(galleryService, galleryMetaData)
    .Render(new SquareTreeRenderer(rendererConfig))
    .Paint(new GoldInterpolator())
    .MapValue(fn)
    .LoadTree(OXDataGenerator5.Id, treeDataProvider);
```

`ReversePipelineBuilder.Create()` initialises an empty `IReversePipelineBuilder` object.

`.GallerySave(...)` create the first requirement and returns an `IReversePipelineBuilder<IPaintedTreeRenderer>`

