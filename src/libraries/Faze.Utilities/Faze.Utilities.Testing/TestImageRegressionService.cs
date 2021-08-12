using Faze.Abstractions.Rendering;
using ImageDiff;
using Shouldly;
using System;
using System.Drawing;
using System.IO;

namespace Faze.Utilities.Testing
{
    public class TestImageRegressionServiceConfig
    {
        public string ExpectedImageDirectory { get; set; }
        public string ImageDiffDirectory { get; set; }
    }

    public class TestImageRegressionService
    {
        private readonly TestImageRegressionServiceConfig config;
        public TestImageRegressionService(TestImageRegressionServiceConfig config)
        {
            this.config = config;
        }

        /// <summary>
        /// Compares the output from a renderer to an existing file.
        /// Deletes diff file if images are equal
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="expectedImageId"></param>
        /// <param name="diffOutputId">If set, the difference will be saved to this location</param>
        /// <returns></returns>
        public void Compare(IPaintedTreeRenderer renderer, string expectedImageId, string diffOutputId = null)
        {
            var comparer = new BitmapComparer();
            var expectedImageFilename = GetExpectedImageFilename(expectedImageId);

            if (!File.Exists(expectedImageFilename))
            {
                throw new Exception($"no expected image exists at '{expectedImageFilename}'");
            }

            using (var expectedBitmap = new Bitmap(expectedImageFilename))
            using (var memoryStream = new MemoryStream())
            {
                renderer.WriteToStream(memoryStream);
                var actualBitmap = new Bitmap(memoryStream);

                var isEqual = comparer.Equals(actualBitmap, expectedBitmap);

                if (diffOutputId != null)
                {
                    var diffFilename = GetImageDiffFilename(diffOutputId);

                    if (!isEqual)
                    {
                        var diff = comparer.Compare(actualBitmap, expectedBitmap);
                        diff.Save(diffFilename);
                    } 
                    else if (File.Exists(diffFilename))
                    {
                        File.Delete(diffFilename);
                    }

                }

                isEqual.ShouldBeTrue();
            }
        }

        internal void GenerateTestCase(IPaintedTreeRenderer renderer, string expectedImageId)
        {
            var filename = GetExpectedImageFilename(expectedImageId);

            using (var fs = File.OpenWrite(filename))
            {
                renderer.WriteToStream(fs);
            }
        }

        private string GetExpectedImageFilename(string id)
        {
            return Path.Combine(config.ExpectedImageDirectory, id);
        }

        private string GetImageDiffFilename(string id)
        {
            return Path.Combine(config.ImageDiffDirectory ?? config.ExpectedImageDirectory, id);
        }
    }
}
