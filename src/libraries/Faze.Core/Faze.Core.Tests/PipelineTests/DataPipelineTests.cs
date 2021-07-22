using Faze.Abstractions.Core;
using Faze.Abstractions.Rendering;
using Faze.Core.Adapters;
using Faze.Core.Data;
using Faze.Core.Extensions;
using Faze.Core.Pipelines;
using Faze.Utilities.Testing;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Faze.Core.Tests.PipelineTests
{
    public class DataPipelineTests
    {
        private readonly ITreeDataWriter<string, object> treeDataWriter;

        public DataPipelineTests()
        {
            this.treeDataWriter = new TestJsonItteratorTreeDataWriter();
        }

        [Theory]
        [InlineData(3, 6)]
        public void PipelineShouldOnlyEnumerateTreeOnce(int size, int depth)
        {
            var dynamicDataProvider = new DynamicTreeDataProvider<object>();
            var dynamicDataOptions = new DynamicSquareTreeOptions<object>(size, depth, info => null); 
            
            var treeMapper = new TestRunCountTreeMapper();
            var pipeline = ReversePipelineBuilder.Create()
                .SaveTree("tmp", treeDataWriter)
                .Map(treeMapper)
                .LimitDepth(depth)
                .LoadTree(dynamicDataOptions, dynamicDataProvider);

            pipeline.Run();

            treeMapper.RunCount.ShouldBe(1);
        }
    }
}
