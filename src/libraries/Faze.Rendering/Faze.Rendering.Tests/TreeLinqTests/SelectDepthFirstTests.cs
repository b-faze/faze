using Faze.Abstractions.Core;
using Faze.Core.TreeLinq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Faze.Rendering.Tests.TreeLinqTests
{
    public class SelectDepthFirstTests
    {
        [Fact]
        public void SingleTreeNodeReturnsSingleItem()
        {
            var singleTree = new Tree<int>(0);
            var result = singleTree.SelectDepthFirst().Count();
        }

        [Fact]
        public void SingleTreeNodeReturnsCorrectValue()
        {
            var expectedValue = 99;
            var singleTree = new Tree<int>(expectedValue);
            
            var result = singleTree.SelectDepthFirst().First();
            result.ShouldBe(expectedValue);
        }

        [Fact]
        public void FullTreeReturnsValuesInCorrectOrder()
        {
            var c34 = new Tree<int>(10);
            var c33 = new Tree<int>(9);
            var c32 = new Tree<int>(8);
            var c31 = new Tree<int>(7);
            var c21 = new Tree<int>(5);
            var c12 = new Tree<int>(3);
            var c11 = new Tree<int>(2);
            var c3 = new Tree<int>(6, new[] { c31, c32, c33, c34 });
            var c2 = new Tree<int>(4, new[] { c21 });
            var c1 = new Tree<int>(1, new[] { c11, c12 });

            var tree = new Tree<int>(0, new[] { c1, c2, c3 });

            var result = tree.SelectDepthFirst().ToArray();
            result.ShouldBe(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        }

    }

}
