using System.Collections.Generic;

namespace Faze.Core.IO
{
    internal class TreeDto
    {
        public string Value { get; set; }
        public IEnumerable<TreeDto> Children { get; set; }
    }
}
