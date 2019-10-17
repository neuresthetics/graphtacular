using Graphtacular.Classes.Kernels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graphtacular.Classes.Drivers
{
    // Driver class runs actions on the Vertices given their Graph.
    // So like graph method integration for batch actions.
    // FUTURE: Operated by Kernels and Motif Generators
    public class DriverZero
    {
        public void SingularUndirectedExtension(Graph graph, Vertex vertex)
        {
            Vertex v = graph.CreateClusterVertex();
            graph.AssimilateVertex(v);
            vertex.G.UndirectedEdge(vertex, v, 1);
        }

        public void SingularUndirectedExtension(Graph graph, List<Vertex> selection)
        {
            List<Vertex> additions = new List<Vertex>();

            foreach (var vertex in selection)
            {
                Vertex v = graph.CreateClusterVertex();
                additions.Add(v);
            }
            graph.AssimilateVertices(additions);
            for (int i = 0; i < additions.Count; i++)
            {
                graph.UndirectedEdge(selection[i], additions[i], 1);
            }
        }

        public void FormRing(Graph graph, List<Vertex> selection, int weight)
        {
            for (int i = 0; i < selection.Count-1; i++)
            {
                graph.UndirectedEdge(selection[i], selection[i + 1], weight);
            }
            graph.UndirectedEdge(selection[0], selection[selection.Count - 1], weight);
        }
    }
}
