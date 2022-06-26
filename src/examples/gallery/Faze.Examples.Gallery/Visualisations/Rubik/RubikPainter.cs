using Faze.Abstractions.Core;
using Faze.Abstractions.GameResults;
using Faze.Abstractions.Rendering;
using Faze.Core.TreeLinq;
using Faze.Rendering.ColorInterpolators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Faze.Examples.Gallery.Visualisations.Rubik
{
    public class RubikPainter : ITreePainter<WinLoseDrawResultAggregate>
    {
        private readonly RubikPainterConfig config;
        private readonly IColorInterpolator colorInterpolator;

        public RubikPainter(RubikPainterConfig config)
        {
            this.config = config;
            this.colorInterpolator = new GoldInterpolator();
        }

        public Tree<Color> Paint(Tree<WinLoseDrawResultAggregate> tree)
        {
            var resultTree = tree
                .MapValue(v => v.GetWinsOverLoses());

            if (config.Normalise)
                resultTree = resultTree.NormaliseSiblings();

            if (config.MappingType == RubikMappingType.ExpandLow)
                resultTree = resultTree.MapValue(v => Math.Log(Math.Sqrt(v) / 0.59 + 1));

            var colouredTree = resultTree
                .MapValue(colorInterpolator);

            if (config.UnavailablePaintType == RubikUnavailablePaintType.Black)
                colouredTree = BlackUnavailableMapping(colouredTree);

            if (config.UnavailablePaintType == RubikUnavailablePaintType.Average)
                colouredTree = AverageUnavailableMapping(colouredTree);

            return colouredTree;
        }

        private Tree<Color> BlackUnavailableMapping(Tree<Color> tree)
        {
            if (tree == null)
            {
                return new Tree<Color>(Color.Black);
            }

            var children = tree.Children?.Select((c, i) => BlackUnavailableMapping(c));

            return new Tree<Color>(tree.Value, children);
        }

        private Tree<Color> AverageUnavailableMapping(Tree<Color> tree, Tree<Color> parent = null)
        {
            if (tree == null && parent == null)
            {
                return null;
            }

            if (tree == null && parent != null)
            {
                var siblings = parent.Children;
                var avgColor = Average(siblings.Where(s => s != null).Select(s => s.Value));

                return new Tree<Color>(avgColor);
            }

            var children = tree.Children?.Select((c, i) => AverageUnavailableMapping(c, tree));

            return new Tree<Color>(tree.Value, children);
        }

        private Color Average(IEnumerable<Color> colors) 
        {
            var totalA = 0;
            var totalR = 0;
            var totalG = 0;
            var totalB = 0;
            var count = 0;

            foreach (var color in colors)
            {
                totalA += color.A;
                totalR += color.R;
                totalG += color.G;
                totalB += color.B;
                count++;
            }

            return Color.FromArgb(totalA / count, totalR / count, totalG / count, totalB / count);
        }
    }
}
