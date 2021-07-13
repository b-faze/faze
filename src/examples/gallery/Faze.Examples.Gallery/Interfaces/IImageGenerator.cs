﻿using Faze.Abstractions.Core;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.Interfaces
{
    public interface IImageGenerator
    {
        Task Generate(IProgressBar progress);
        ImageGeneratorMetaData GetMetaData();
    }
}