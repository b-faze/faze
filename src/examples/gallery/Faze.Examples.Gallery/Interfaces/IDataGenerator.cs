using Faze.Abstractions.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faze.Examples.Gallery.Interfaces
{
    public interface IDataGenerator
    {
        string Id { get; }
        Task Generate(IProgressBar progress);
    }
}
