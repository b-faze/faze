using System;
using System.Windows.Forms;

namespace Faze.Rendering.Playground
{
    internal abstract class RecursiveTemplateChildArea
    {
        public RecursiveTemplateChildArea(string id)
        {
            Id = id;
        }

        public string Id { get; }

        internal abstract bool Intercepts(float x, float y);

        internal abstract void Move(float x, float y);

        internal abstract (float x, float y) GetMovePoint();

        internal abstract bool WithinResizeUI(float x, float y);

        internal abstract Cursor GetResizeCursor(float x, float y);
    }
}
