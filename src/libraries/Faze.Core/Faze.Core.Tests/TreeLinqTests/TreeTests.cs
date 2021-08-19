using Faze.Abstractions.Core;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Faze.Core.Tests.TreeLinqTests
{
    public class TreeTests
    {
        [Fact]
        public void TreeDoesNotEqualNull()
        {
            var tree = new Tree<int>(0);
            var tree2 = new Tree<object>(null);

            tree.Equals(null).ShouldBeFalse();
            tree2.Equals(null).ShouldBeFalse();
        }

        [Fact]
        public void DifferentTreeGenericTypeNotEqual()
        {
            var tree = new Tree<int>(0);
            var tree2 = new Tree<double>(0);

            tree.Equals(tree2).ShouldBeFalse();
            tree2.Equals(tree).ShouldBeFalse();
        }

        [Fact]
        public void TreeValueDoesNotEqual()
        {
            var tree = new Tree<int>(0);
            var tree2 = new Tree<int>(1);

            tree.Equals(tree2).ShouldBeFalse();
            tree2.Equals(tree).ShouldBeFalse();
        }

        [Fact]
        public void TreeValueDoesEqual()
        {
            var tree = new Tree<int>(99);
            var tree2 = new Tree<int>(99);

            tree.Equals(tree2).ShouldBeTrue();
            tree2.Equals(tree).ShouldBeTrue();
        }
    }
}
