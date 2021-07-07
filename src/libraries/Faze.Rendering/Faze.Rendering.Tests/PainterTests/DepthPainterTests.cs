using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Rendering.TreePainters;
using Faze.Rendering.TreeRenderers;
using Faze.Rendering.Tests.Utilities;
using System.Drawing.Imaging;
using Xunit;
using Faze.Rendering.Extensions;

namespace Faze.Rendering.Tests.PainterTests
{
    public class DepthPainterTests
    {
        private Tree<int> tree;
        private IPaintedTreeRenderer renderer;

        public DepthPainterTests()
        {
            var treeSize = 1;
            this.tree = TreeUtilities.CreateSquareTree(treeSize, 10);
            this.renderer = new SquareTreeRenderer(new SquareTreeRendererOptions(treeSize)
            {
                BorderProportions = 0.1f
            }, 600);
        }

        [Fact]
        public void DrawDefaultDepthPainter()
        {
            var depthPainter = new DepthPainter();
            var paintedTree = depthPainter.Paint(tree);
            var filename = FileUtilities.GetTestOutputPath(nameof(DepthPainterTests),
                $"{nameof(DrawDefaultDepthPainter)}.png");

            renderer.Draw(paintedTree);
            renderer.Save(filename);

        }

        [Fact]
        public void DrawReverseDepthPainter()
        {
            var depthPainter = new DepthPainter();
            var paintedTree = depthPainter.Paint(tree);
            var filename = FileUtilities.GetTestOutputPath(nameof(DepthPainterTests),
                $"{nameof(DrawReverseDepthPainter)}.png");

            renderer.Draw(paintedTree);
            renderer.Save(filename);
        }
    }
}
