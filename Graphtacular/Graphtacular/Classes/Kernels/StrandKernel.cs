using System;
using System.Collections.Generic;
using System.Text;

namespace Graphtacular.Classes.Kernels
{
    public class StrandKernel : BaseKernel
    {
        public override Vertex V { get; set; }

        // KERNEL SWITCH
        public override void Run(Dictionary<int, object[]> strand, int step)
        {
            /// record: ~7657 vertices, ~2.5 min: ~51 verts a second.
            // safety = record/2
            if (V.G.AllVertices.Count > 3828) { return; }

            step++;
            // loop termination
            if (!strand.ContainsKey(step)) { return; }


            // extract directive: [0]: Function [{>=1}] Props
            object[] ss = strand[step];

            // select kernel method, pass arguments
            switch (ss[0])
            {
                case "KbranchUndirected":
                    KbranchUndirected(strand, step, Convert.ToInt32(ss[1]), Convert.ToInt32(ss[2]));
                    break;

                case "KcompleteCluster":
                    KcompleteCluster(strand, step, Convert.ToInt32(ss[1]), Convert.ToInt32(ss[2]));
                    break;

                default:
                    break;
            }
        }

        // KERNEL METHODS // these may need to decoupled into drivers?
        public void KbranchUndirected(Dictionary<int, object[]> strand, int step, int children, int weight)
        {
            for (int i = 0; i < children; i++)
            {
                Vertex v = V.G.AddStrandVertex();
                V.G.UndirectedEdge(V, v, 1);
                v.K.Run(strand, step);
            }
        }

        public void KcompleteCluster(Dictionary<int, object[]> strand, int step, int children, int weight)
        {
            List<Vertex> batch = new List<Vertex>();
            for (int i = 0; i < children; i++)
            {
                Vertex v = V.G.AddStrandVertex();
                V.G.UndirectedEdge(V, v, weight);
                batch.Add(v);
                v.K.Run(strand, step);
            }
            V.G.FullConnectSet(batch, weight);
        }
    }
}
