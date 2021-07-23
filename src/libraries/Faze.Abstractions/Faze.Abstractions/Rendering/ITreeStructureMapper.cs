using Faze.Abstractions.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Abstractions.Rendering
{
    public interface ITreeStructureMapper
    {
        Tree<T> Map<T>(Tree<T> tree, IProgressTracker progress);
    }
}
