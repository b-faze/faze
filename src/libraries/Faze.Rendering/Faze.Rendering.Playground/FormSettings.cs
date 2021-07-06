using System;
using System.Linq;

namespace Faze.Rendering.Playground
{
    public class FormSettings
    {
        public OptionPreset[] Presets { get; set; }

        internal void UpdatePreset(string presetName, Options options)
        {
            var existing = Presets.FirstOrDefault(x => x.Name == presetName);
            if (existing != null)
            {
                existing.Options = options;
            }
            else
            {
                Presets = Presets.Concat(new[]
                {
                    new OptionPreset {
                        Name = presetName,
                        Options = options
                    }
                }).ToArray();
            }
        }
    }
}
