# faze

<img align="center" src="docs/banner.png" alt="Banner" />

---

| Package           | NuGet |
|-------------------|-------|
| Faze.Abstractions | [![NuGet](https://img.shields.io/nuget/v/Faze.Abstractions.svg)](https://www.nuget.org/packages/Faze.Abstractions/) |
| Faze.Core         | [![NuGet](https://img.shields.io/nuget/v/Faze.Core.svg)](https://www.nuget.org/packages/Faze.Core/) |
| Faze.Core.IO         | [![NuGet](https://img.shields.io/nuget/v/Faze.Core.IO.svg)](https://www.nuget.org/packages/Faze.Core.IO/) |
| Faze.Engine       | [![NuGet](https://img.shields.io/nuget/v/Faze.Engine.svg)](https://www.nuget.org/packages/Faze.Engine/) |
| Faze.Rendering    | [![NuGet](https://img.shields.io/nuget/v/Faze.Rendering.svg)](https://www.nuget.org/packages/Faze.Rendering/) |

## About

faze is a collection of NuGet packages, providing tools and pipelines for visualising game logic as images. For more information please check out the gitbook at https://b-hub.gitbook.io/faze/ for documentation, examples and blogs.

## Gallery

See an example gallery online at https://b-faze.github.io/faze/

Everything in the gallery is produced from this repository in Faze.Examples.Gallery.CLI, try it for yourself! The Gallery CLI splits the visualisation pipelines in half allowing data to be pre-computing (`generate-data`) to help speed up rendering (`generate-images`)

```
 Usages:
   generate-data [options]
   generate-images [options]
   check-images [pptions]
   
 generate-data options:
   --id         Generates data for a given id  [string]
  
 generate-images options:
   --album      Generates images for a given album  [string]
  
 check-images options:
   
```
