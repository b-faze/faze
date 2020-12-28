using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faze.Rendering.Playground
{
    internal class RecursiveTemplate
    {
        private readonly IList<RecursiveTemplateChildArea> children;

        public RecursiveTemplate()
        {
            this.children = new List<RecursiveTemplateChildArea>();
        }

        public IEnumerable<RecursiveTemplateChildArea> Children => children;

        public void AddChild(RecursiveTemplateChildArea child)
        {
            children.Add(child);
        }

        internal void Clear()
        {
            children.Clear();
        }

        internal RecursiveTemplateChildArea GetTopChildAt(float x, float y)
        {
            return children.LastOrDefault(child => child.Intercepts(x, y));
        }
    }
}
