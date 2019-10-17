using System;
using System.Collections.Generic;
using System.Text;

namespace Graphtacular.Classes.Kernels
{
    public abstract class BaseKernel
    {
        public virtual Vertex V { get; set; }

        public abstract void Run(Dictionary<int, object[]> strand, int step);
    }
}
