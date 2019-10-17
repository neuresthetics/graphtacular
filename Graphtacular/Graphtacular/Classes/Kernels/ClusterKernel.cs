using System;
using System.Collections.Generic;
using System.Text;

namespace Graphtacular.Classes.Kernels
{
    public class ClusterKernel : BaseKernel
    {
        // My thought here is that clusters should be managed externally by cluster Drivers.
        // I suppose that ClusterKernel generations could be managed as clusters by the same cluster Drivers.
        // So eventually there will need to be a balance between the two, like nature and nurture.
        // Perhaps StrandKernels of certin functions should have access to cluster Drivers of some scope.
        
        // This lightweight extension of BaseKernel is mostly to reduce instantiation requirements for dev/testing.

        public override Vertex V { get; set; }

        public override void Run(Dictionary<int, object[]> strand, int step)
        {
            throw new NotImplementedException();
        }
    }
}
