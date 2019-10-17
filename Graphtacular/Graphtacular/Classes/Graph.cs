using Graphtacular.Classes.Kernels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//!
namespace Graphtacular.Classes
{
    public class Graph
    {
        // registry
        public List<Vertex> AllVertices = new List<Vertex>();
        // registry count
        public int size = 0;

        public int edgeCount = 0;
        // adjacency matrix
        public List<List<int>> Matrix = new List<List<int>>();
        // vertex-guid to matrix position translation
        public Dictionary<Guid, int> MatrixKey = new Dictionary<Guid, int>();

        /// <summary>
        ///     console output
        /// </summary>
        public void PrintMatrix()
        {
            Console.WriteLine("MATRIX");
            StringBuilder output = new StringBuilder();

            output.Append("XXXXXXXX-GUID-XXXX-XXXX-XXXXXXXXXXXX       ");
            output.Append("[ ");

            // X axis display
            for (int i = 0; i < Matrix.Count; i++)
            {
                output.Append($"{i} ");
                if (i < 10) { output.Append(" "); }
            }
            output.Append("]");
            output.Append("\n");
            output.Append("\n");

            int j = 0; // number for Y axis display

            foreach (var row in Matrix)
            {
                output.Append($"{AllVertices[j++].ID}");

                // spacing between vertex index and connection row
                if (j <= 10) { output.Append("  "); }
                else if (j < 100) { output.Append(" "); }
                output.Append($" [{j - 1}] ");

                output.Append("[ ");
                foreach (var conn in row)
                {
                    output.Append($"{conn}  ");
                }
                output.Append("]");
                //output.Append($" -- {AllVertices[j - 1].K.GetType()}");
                output.Append("\n");
            }

            Console.WriteLine(output.ToString());
        }

        /// <summary>
        ///     console output
        /// </summary>
        public void PrintVertices()
        {
            Console.WriteLine("VERTICES");
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < AllVertices.Count; i++)
            {
                output.Append($"V[{i}]:{AllVertices[i].ID} K: {AllVertices[i].K}");
                output.Append("\n");
            }
            Console.WriteLine(output.ToString());
        }

        /// <summary>
        ///     console output
        /// </summary>
        public void PrintEdges()
        {
            Console.WriteLine("EDGES:");
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < AllVertices.Count; i++)
            {
                output.Append($"V[{i}]: ");
                for (int j = 0; j < AllVertices.Count; j++)
                {
                    if (Matrix[j][i] != 0)
                    {
                        output.Append($"(v[{j}]:");
                        output.Append($"{Matrix[j][i]}) ");
                    }
                }
                output.Append("\n");
            }
            Console.WriteLine(output.ToString());
        }

        /// <summary>
        ///     associates a Vertex ID to a MatrixKey
        /// </summary>
        /// <param name="v"></param>
        private void RegisterKey(Vertex v)
        {
            MatrixKey.Add(v.ID, size);
        }

        /// <summary>
        ///     updates matrix with row and columb
        /// </summary>
        /// <param name="v"></param>
        private void MatrixEntry(Vertex v)
        {
            // new row
            List<int> entry = new List<int>();

            // populate row to new matrix width (++prior)
            for (int i = 0; i < size; i++) { entry.Add(0); };

            // append entry
            Matrix.Add(entry);

            // new col above new row
            if (size > 1)
            {
                for (int i = 0; i < Matrix.Count - 1; i++)
                {
                    Matrix[i].Add(0);
                }
            }
        }

        /// <summary>
        ///     addition of a vertex
        /// </summary>
        /// <returns>Vertex added</returns>
        public Vertex AddStrandVertex()
        {
            // ! Vertices should share a single Kernel.
            StrandKernel k = new StrandKernel();
            Vertex v = new Vertex(k);
            // holographic association
            v.G = this;
            k.V = v;

            // register
            AllVertices.Add(v);
            RegisterKey(v);
            size++;

            // dependent on new size:
            MatrixEntry(v);

            // this little relicis from the first recursive run. There could be a case where each vertex added would be spun up automatically.
            // I think this might apply if multithreading allowed vertices to operate with distributed stack capacities?
            //v.K.Run();
            return v;
        }

        /// <summary>
        ///     addition of a vertex
        /// </summary>
        /// <returns>Vertex added</returns>
        public Vertex AddClusterVertex()
        {
            // material
            ClusterKernel k = new ClusterKernel();
            Vertex v = new Vertex(k);
            // holographic association
            v.G = this;
            k.V = v;

            // register
            AllVertices.Add(v);
            RegisterKey(v);
            size++;

            // dependent on new size:
            MatrixEntry(v);

            // this little relicis from the first recursive run. There could be a case where each vertex added would be spun up automatically.
            // I think this might apply if multithreading allowed vertices to operate with distributed stack capacities?

            //v.K.Run();

            return v;
        }

        /// <summary>
        ///     Removes Vertex from matrix | keys | list | count
        /// </summary>
        /// <param name="v">Vertex to be deleted</param>
        public void DeleteVertex(Vertex v)
        {
            // remove from matrix
            MatrixRemoval(v);
            // delete key
            DeleteMatrixKey(v);
            // delete from all verts
            AllVertices.Remove(v);
            // count continuity with keys
            size--;
        }

        /// <summary>
        ///     Removes target Vertex from Matrix
        /// </summary>
        /// <param name="v">Target vertex</param>
        public void MatrixRemoval(Vertex v)
        {

            //edgeCount--;
            // everything it connects to
            for (int i = 0; i < AllVertices.Count; i++)
            {
                Matrix[i].RemoveAt(MatrixKey[v.ID]);
            }
            // everything that connects to it
            Matrix.RemoveAt(MatrixKey[v.ID]);

        }

        /// <summary>
        ///     Remove Vertex from MatrixKeys | mirror matrix contraction
        /// </summary>
        /// <param name="v"></param>
        public void DeleteMatrixKey(Vertex v)
        {
            List<Guid> keySet = new List<Guid>(MatrixKey.Keys);

            foreach (var key in keySet)
            {
                // decrement key values for count continuity with key values and matrix size
                if(MatrixKey[key] > MatrixKey[v.ID])
                {
                    MatrixKey[key]--;
                }
            }

            MatrixKey.Remove(v.ID);
        }

        /// <summary>
        ///     Combines two Vertices on a new one, preserving their degrees.
        /// </summary>
        /// <param name="v1">Vert 1</param>
        /// <param name="v2">Vert 2</param>
        /// <returns>new Vertex carrying edges</returns>
        public Vertex JoinVertexPairUndirected(Vertex v1, Vertex v2)
        {
            Vertex newVert = AddStrandVertex();
            List<Vertex> v1neighbors = NeighborSet(v1);
            List<Vertex> v2neighbors = NeighborSet(v2);

            foreach (var vert in v1neighbors)
            {
                UndirectedEdge(vert, newVert, Matrix[MatrixKey[vert.ID]][MatrixKey[v1.ID]]);
            }
            foreach (var vert in v2neighbors)
            {
                UndirectedEdge(vert, newVert, Matrix[MatrixKey[vert.ID]][MatrixKey[v2.ID]]);
            }

            DeleteVertex(v1);
            DeleteVertex(v2);

            return newVert;
        }

        /// <summary>
        ///     Mirrors a set of Vertices and their edges.
        /// </summary>
        /// <param name="set">set to be mirrored</param>
        /// <returns>Mirrored set</returns>
        public List<Vertex> DoppleSet(List<Vertex> set)
        {
            List<Vertex> dopple = new List<Vertex>();
            foreach (var vert in set)
            {
                dopple.Add(AddStrandVertex());
            }

            for (int i = 0; i < set.Count; i++)
            {
                for (int j = 0; j < set.Count; j++)
                {
                    // consider removing this if on forceful overwrite
                    if (Matrix[MatrixKey[set[i].ID]][MatrixKey[set[j].ID]] != 0)
                    {
                        Matrix[MatrixKey[dopple[i].ID]][MatrixKey[dopple[j].ID]] = Matrix[MatrixKey[set[i].ID]][MatrixKey[set[j].ID]];
                    }
                }
            }
            return dopple;
        }

        /// <summary>
        ///    freestanding Vertex with cluster Kernel
        /// </summary>
        /// <returns></returns>
        public Vertex CreateClusterVertex()
        {
            ClusterKernel k = new ClusterKernel();
            Vertex v = new Vertex(k);
            return v;
        }

        /// <summary>
        ///    freestanding Vertex with strand Kernel
        /// </summary>
        /// <returns></returns>
        public Vertex CreateStrandVertex()
        {
            StrandKernel k = new StrandKernel();
            Vertex v = new Vertex(k);
            return v;
        }

        /// <summary>
        ///     add, register, count, and enter matrix
        /// </summary>
        /// <param name="v">target Vertex</param>
        /// <returns>Vertex assimilated</returns>
        public Vertex AssimilateVertex(Vertex v)
        {
            AllVertices.Add(v);
            RegisterKey(v);
            size++;

            // dependent on new size:
            MatrixEntry(v);
            return v;
        }

        /// <summary>
        ///     assimilate set of Verteces
        /// </summary>
        /// <param name="verts">List Vertex to be added</param>
        public void AssimilateVertices(List<Vertex> verts)
        {
            foreach (var v in verts)
            {
                AssimilateVertex(v);
            }
        }

        /// <summary>
        ///     directed edge between two Vertices
        /// </summary>
        /// <param name="pointing">from</param>
        /// <param name="pointed">to</param>
        /// <param name="weight">connection weight</param>
        public void DirectedEdge(Vertex pointing, Vertex pointed, int weight)
        {
            // use guid of inputs to grab matrix location
            int p1 = MatrixKey[pointing.ID];
            int p2 = MatrixKey[pointed.ID];
            Matrix[p2][p1] = weight;
            // ! graph type warning
            edgeCount++;
        }

        /// <summary>
        ///     undirected edge from one Vertex to another.
        /// </summary>
        /// <param name="a">vert1</param>
        /// <param name="b">vert2</param>
        /// <param name="weight">connection weight</param>
        public void UndirectedEdge(Vertex a, Vertex b, int weight)
        {
            DirectedEdge(a, b, weight);
            DirectedEdge(b, a, weight);
            // ! graph type warning
            edgeCount++;
            edgeCount++;
        }

        /// <summary>
        ///     count and return edges from a Vertex
        /// </summary>
        /// <param name="v">target Vertex</param>
        /// <returns>int count</returns>
        public int OutDegreeCount(Vertex v)
        {
            int count = 0;
            foreach (var row in Matrix)
            {
                if (row[MatrixKey[v.ID]] != 0)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        ///     collect and return vertices pointed to from a vertex
        /// </summary>
        /// <param name="v">target Vertex</param>
        /// <returns>List Vertices</returns>
        public List<Vertex> OutDegreeVertices(Vertex v)
        {
            List<Vertex> outVerts = new List<Vertex>();
            int intersection = 0;

            foreach (var row in Matrix)
            {
                if (row[MatrixKey[v.ID]] != 0)
                {
                    outVerts.Add(AllVertices[intersection]);
                }
                intersection++;
            }
            return outVerts;
        }

        /// <summary>
        ///     count and return edges to a Vertex
        /// </summary>
        /// <param name="v">target Vertex</param>
        /// <returns>int count</returns>
        public int InDegreeCount(Vertex v)
        {
            int count = 0;
            foreach (var col in Matrix[MatrixKey[v.ID]])
            {
                if (col != 0)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        ///     collect and return vertices pointing to a vertex
        /// </summary>
        /// <param name="v">target Vertex</param>
        /// <returns>List Vertex</returns>
        public List<Vertex> InDegreeVertices(Vertex v)
        {
            List<Vertex> inVerts = new List<Vertex>();
            int intersection = 0;

            // TODO: ! possible bug when using neighborCount method.
            foreach (var col in Matrix[MatrixKey[v.ID]])
            {
                if (col != 0)
                {
                    inVerts.Add(AllVertices[intersection]);
                }
                intersection++;
            }
            return inVerts;
        }

        /// <summary>
        ///     sum of a Vertex indegree and outdegree
        /// </summary>
        /// <param name="v">target Vertex</param>
        /// <returns>int sum</returns>
        public int DirectedEdgeCount(Vertex v)
        {
            return InDegreeCount(v) + OutDegreeCount(v);
        }

        /// <summary>
        ///     for undirected, count mutual connections
        /// </summary>
        /// <param name="v">target Vertex</param>
        /// <returns>int mutual edges</returns>
        public int MutualEdgeCount(Vertex v)
        {
            List<Vertex> indegree = InDegreeVertices(v);
            List<Vertex> outdegree = OutDegreeVertices(v);

            return indegree.Intersect(outdegree).Count();
        }

        /// <summary>
        ///     direction agnostic neighbor count
        /// </summary>
        /// <param name="v">target Vertex</param>
        /// <returns>int neighbor count</returns>
        public int NeighborCount(Vertex v)
        {
            int indegree = InDegreeCount(v);
            int outdegree = OutDegreeCount(v);

            return indegree + outdegree - MutualEdgeCount(v);
        }

        /// <summary>
        ///     direction agnostic neighbor list
        /// </summary>
        /// <param name="v">target Vertex</param>
        /// <returns>List Vertex neighbors</returns>
        public List<Vertex> NeighborSet(Vertex v)
        {
            List<Vertex> indegreeSet = InDegreeVertices(v);
            List<Vertex> outdegreeSet = OutDegreeVertices(v);

            return indegreeSet.Concat(outdegreeSet.Except(indegreeSet)).ToList();
        }

        /// <summary>
        ///     nonconnected Vertex count
        /// </summary>
        /// <returns>int island count</returns>
        public int IslandCount()
        {
            int count = 0;
            foreach (var v in AllVertices)
            {
                if (NeighborCount(v) == 0)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        ///     nonconnected Vertex list
        /// </summary>
        /// <returns>List Vertex islands</returns>
        public List<Vertex> IslandSet()
        {
            List<Vertex> islands = new List<Vertex>();
            foreach (var v in AllVertices)
            {
                if (NeighborCount(v) == 0)
                {
                    islands.Add(v);
                }
            }
            return islands;
        }

        /// <summary>
        ///     collect refrence to all Vertices within K of a target
        /// </summary>
        /// <param name="v">target Vertex</param>
        /// <param name="k">int distance</param>
        /// <returns>List Vertex</returns>
        public List<Vertex> NeighborsWithinK(Vertex v, int k)
        {
            if (k < 1) { Console.WriteLine("null"); return null; }

            List<Vertex> visited = new List<Vertex>();
            List<Vertex> currentOrigin = new List<Vertex>() { v };
            List<Vertex> currentOrbit = new List<Vertex>();

            for (int i = 0; i < k; i++)
            {
                foreach (var vert in currentOrigin)
                {
                    foreach (var ver in NeighborSet(vert))
                    {
                        if (!visited.Contains(ver))
                        {
                            currentOrbit.Add(ver);
                        }
                    }
                }
                foreach (var vert in currentOrigin)
                {
                    if (!visited.Contains(vert))
                    {
                        visited.Add(vert);
                    }

                }
                currentOrigin.Clear();
                foreach (var vert in currentOrbit)
                {
                    if (!visited.Contains(vert))
                    {
                        currentOrigin.Add(vert);
                    }
                }
                currentOrbit.Clear();
            }
            foreach (var vert in currentOrigin)
            {
                if (!visited.Contains(vert))
                {
                    visited.Add(vert);
                }
            }

            visited.Remove(v);
            return visited;
        }

        /// <summary>
        ///     calculate the max potential global undirected edges
        /// </summary>
        /// <param name="n">number of Vertices</param>
        /// <returns>int max connections</returns>
        public int MaxConnections(int n)
        {
            if (n <= 1) { return 0; }
            int past = 1;
            int tail = 3;
            int lead = 6;
            if (n == 2) { return past; }
            if (n == 3) { return tail; }
            if (n == 4) { return lead; }

            while (n-- > 4)
            {
                int next = lead * 2 - tail + 1;
                past = tail;
                tail = lead;
                lead = next;
            }
            return lead;
        }

        /// <summary>
        ///     check for edge to self
        /// </summary>
        /// <param name="v">target Vertex</param>
        /// <returns></returns>
        public bool IsSelfRefrence(Vertex v)
        {
            int p = MatrixKey[v.ID];
            if (Matrix[p][p] != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///     removes edge to self
        /// </summary>
        /// <param name="v">target Vertex</param>
        public void PurgeSelfRefrence(Vertex v)
        {
            int p = MatrixKey[v.ID];
            Matrix[p][p] = 0;
        }

        /// <summary>
        ///     removes multigraph
        /// </summary>
        /// <param name="v">self refrences removed</param>
        public int PurgeSelfReferences()
        {
            int count = 0;
            for (int i = 0; i < Matrix.Count; i++)
            {
                for (int j = 0; j < Matrix.Count; j++)
                {
                    if (Matrix[i][j] != 0)
                    {
                        count++;
                        Matrix[i][j] = 0;
                    }
                }
            }
            return count;
        }

        /// <summary>
        ///     complete undirected connection for a set of Vertices to eachother
        /// </summary>
        /// <param name="set">Vertices to connect</param>
        /// <param name="strength">weight of connection</param>
        /// <returns>connections made</returns>
        public int FullConnectSet(List<Vertex> set, int strength)
        {
            int count = 0;
            Dictionary<Vertex, List<Vertex>> connectome = new Dictionary<Vertex, List<Vertex>>();
            foreach (var v in set)
            {
                List<Vertex> copy = new List<Vertex>(set);
                connectome[v] = copy;
                connectome[v].Remove(v);
            }
            foreach (var subset in connectome)
            {
                foreach (var neighbor in subset.Value)
                {
                    count++;
                    UndirectedEdge(subset.Key, neighbor, strength);
                    connectome[neighbor].Remove(subset.Key);
                }
            }
            return count;
        }

        /// <summary>
        ///     count the number of related Vertices in a set
        /// </summary>
        /// <param name="set">List to check</param>
        /// <returns>int pair relations</returns>
        public int ConnectionsBetweenCount(List<Vertex> set)
        {
            Dictionary<Vertex, List<Vertex>> connectome = new Dictionary<Vertex, List<Vertex>>();
            foreach (var v in set) { connectome[v] = NeighborSet(v).Intersect(set).ToList(); }
            int count = 0;

            foreach (var subset in connectome)
            {
                foreach (var neighbor in subset.Value)
                {
                    count++;
                    connectome[neighbor].Remove(subset.Key);
                }
            }
            return count;
        }

        /// <summary>
        ///     measures how connected a Vertex's neightobrs are to eachother
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public decimal NondirectionalClusteringCoefficient(Vertex v)
        {
            List<Vertex> n = NeighborSet(v);
            return decimal.Divide(ConnectionsBetweenCount(n), MaxConnections(n.Count()));
        }

        // graph network methods

        // count edges mutual symmetrical
        // count edges mutual asymmetrical

        // Assortativity
        // Characteristic path length
        // Effective connectivity
        // Path Length
        // Reachability matrix
        // Dikstras list affinity
        // Prims!! list affinity
        // Floyd
        // Warshall

        // graph vertex methods
        // Centrality
        // Degree
        // Diameter
        // Distance
        // Stregnth
        // Kernel reassignment
        // Kernel swap?

        // ...
    }
}
