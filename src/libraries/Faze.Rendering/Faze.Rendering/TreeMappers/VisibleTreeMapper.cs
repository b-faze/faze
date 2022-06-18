using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faze.Rendering.TreeMappers
{
    public class VisibleTreeMapper : ITreeStructureMapper
    {
        private readonly IPaintedTreeRenderer renderer;

        public VisibleTreeMapper(IPaintedTreeRenderer renderer)
        {
            this.renderer = renderer;
        }

        public Tree<T> Map<T>(Tree<T> tree, IProgressTracker progress = null)
        {
            return renderer.GetVisible(tree);
        }
    }
}
