using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Faze.Rendering.Tests.Utilities
{
    public class FileUtilities
    {
        public static string GetTestOutputPath(string testClassName, string filename)
        {
            var directory = $"../../../TestOutputs/{testClassName}";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return Path.Combine(directory, filename);
        }
    }
}
