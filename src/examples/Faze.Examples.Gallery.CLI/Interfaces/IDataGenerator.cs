using Faze.Abstractions.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.CLI.Interfaces
{
    public interface IDataGenerator
    {
        Task Generate(IProgressBar progress);
    }
}
