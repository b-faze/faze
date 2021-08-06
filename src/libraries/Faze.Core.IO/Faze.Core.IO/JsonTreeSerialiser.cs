using Faze.Abstractions.Core;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace Faze.Core.IO
{
    public class JsonTreeSerialiser<T> : ITreeSerialiser<T>
    {
        private readonly IValueSerialiser<T> valueSerialiser;

        public JsonTreeSerialiser(IValueSerialiser<T> valueSerialiser)
        {
            this.valueSerialiser = valueSerialiser;
        }

        public Tree<T> Deserialize(TextReader textReader)
        {
            var serializer = new JsonSerializer();
            TreeDto treeDto;
            using (var reader = new JsonTextReader(textReader))
            {
                treeDto = serializer.Deserialize<TreeDto>(reader);
            }

            return ToTree(treeDto);
        }

        public void Serialize(TextWriter textWriter, Tree<T> tree, IProgressTracker progress)
        {
            var serializer = new JsonSerializer();
            using (var writer = new JsonTextWriter(textWriter))
            {
                serializer.Serialize(writer, ToDto(tree));
            }
        }

        private TreeDto ToDto(Tree<T> tree)
        {
            if (tree == null)
                return null;

            var value = valueSerialiser.Serialize(tree.Value);
            var children = tree.Children?.Select(child => ToDto(child));

            return new TreeDto
            {
                Value = value,
                Children = children
            };
        }

        private Tree<T> ToTree(TreeDto treeDto)
        {
            if (treeDto == null)
                return null;

            var value = valueSerialiser.Deserialize(treeDto.Value);
            var children = treeDto.Children?.Select(child => ToTree(child)).ToArray();

            return new Tree<T>(value, children);
        }
    }
}
