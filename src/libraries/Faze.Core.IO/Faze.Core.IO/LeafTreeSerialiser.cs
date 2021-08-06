using Faze.Abstractions.Core;
using Faze.Core.Extensions;
using Faze.Core.TreeLinq;
using System;
using System.IO;
using System.Linq;

namespace Faze.Core.IO
{
    public class LeafTreeSerialiserConfig<T>
    {
        public AddTree<T> AddTree { get; set; }
    }

    public class LeafTreeSerialiser<T> : ITreeSerialiser<T>
    {
        private readonly IValueSerialiser<T> valueSerialiser;
        private readonly LeafTreeSerialiserConfig<T> config;

        public LeafTreeSerialiser(IValueSerialiser<T> valueSerialiser, LeafTreeSerialiserConfig<T> config = null)
        {
            this.valueSerialiser = valueSerialiser;
            this.config = config ?? new LeafTreeSerialiserConfig<T>();
        }

        public Tree<T> Deserialize(TextReader textReader)
        {
            var tree = config.AddTree ?? new AddTree<T>(new AddTreeConfig<T>());

            string line = null;
            while ((line = textReader.ReadLine()) != null)
            {
                var parts = line.Split(':');
                var path = parts[0].Split(',').Select(int.Parse).ToArray();
                var value = valueSerialiser.Deserialize(parts[1]);

                tree.Add(path, value);
            }

            return tree;
        }

        public void Serialize(TextWriter textWriter, Tree<T> tree, IProgressTracker progress)
        {
            var leaves = tree
                .WithInfo()
                .GetLeaves()
                .Select(t => t.Value);

            foreach (var (value, info) in leaves)
            {
                progress?.SetMessage(info.GetPathHash());

                var line = $"{info.GetPathHash()}:{valueSerialiser.Serialize(value)}";
                textWriter.WriteLine(line);
            }
        }
    }
}
