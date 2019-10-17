using Graphtacular.Classes.Kernels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graphtacular.Classes
{
    public class Vertex
    {
        public Guid ID = Guid.NewGuid();
        public BaseKernel K { get; set; }
        public Graph G { get; set; }

        public Vertex(BaseKernel k)
        {
            K = k;
        }

        public int GetIndex()
        {
            return G.MatrixKey[ID];
        }
    }
}
