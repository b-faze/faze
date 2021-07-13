using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using System.Drawing;
using Faze.Core.TreeLinq;
using Faze.Rendering.ColorInterpolators;
using System.Linq;
using Faze.Examples.Gallery.Services.Aggregates;

namespace Faze.Examples.Gallery.Visualisations.EightQueensProblem
{
    public class EightQueensProblemPainter : ITreePainter<EightQueensProblemSolutionAggregate>
    {
        private IColorInterpolator colorInterpolator;

        public EightQueensProblemPainter()
        {
            this.colorInterpolator = new GoldInterpolator();
        }

        public Tree<Color> Paint(Tree<EightQueensProblemSolutionAggregate> tree)
        {
            var result = Paint(tree, TreeMapInfo.Root(), tree.Value.Wins);

            return result;
        }

        private Tree<Color> Paint(Tree<EightQueensProblemSolutionAggregate> tree, TreeMapInfo info, uint maxSiblingWins)
        {
            if (tree == null)
            {
                return info.Parent.ChildIndex == info.ChildIndex ? null : new Tree<Color>(Color.Black);
            }

            var value = maxSiblingWins > 0 ? (double)tree.Value.Wins / maxSiblingWins : 0;
            var color = value > 0 ? colorInterpolator.GetColor(value) : Color.Black;

            if (tree.IsLeaf())
                return new Tree<Color>(color);

            var maxChildrenWins = tree.Children.Where(c => c != null).Select(x => x.Value.Wins).Max();
            var children = tree.Children.Select((c, i) => Paint(c, info.Child(i), maxChildrenWins));

            return new Tree<Color>(color, children);
        }
    }
}
