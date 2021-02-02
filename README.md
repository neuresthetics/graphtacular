# GRAPHTACULAR

![Render](https://github.com/jasonb315/graphtacular/blob/master/assets/screenshot_100214.png)<br>

![Whiteboard](https://github.com/jasonb315/graphtacular/blob/master/assets/WB00.jpg) <br>

This Graph has Vertices which have Kernels which are given Strands.

Strands passed into Kernels, which contain a refrence to the Vertex that are inside.

Vertices have refrences to the matrix they are inside.

So a Kernel can operate in terms of all the Vertices in the graph it's in.

The Graph is structured like this:

```
 public class Graph
    {
        // registry
        public List<Vertex> AllVertices = new List<Vertex>();
        // registry count
        public int size = 0;
        // adjacency matrix
        public List<List<int>> Matrix = new List<List<int>>();
        // vertex-guid to matrix position translation
        public Dictionary<Guid, int> MatrixKey = new Dictionary<Guid, int>();
```

A Vertex looks something like this:

```
public class Vertex
    {
        public Guid ID = Guid.NewGuid();
        public BaseKernel K { get; set; }
        public Graph cluster { get; set; }

        public Vertex(BaseKernel k)
        {
            K = k;
        }
    }
```

A Vertex has a Kernel object which gives it the ability to execute functions on itself.

A Strand is a series of instructions, in this case, the seed vertex is given the entire strand, which generates a linked list with a blob on the end:

```
public void Flower()
        {
            int n = 1;
            // flowers
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KcompleteCluster", 4, 7 }); n++;
            flower.Add(n, new object[] { null });
        }
```

The strand passed to the Kernel, which parses it and calls logic on its shell Vertex:

```
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
```

Kernels are used to generate pre-programmed motifs. The above strand passed to a single seed Vertex produces the following matrix and graph:

## GEXF - save graph to file
You can view saved [graphs](https://github.com/jasonb315/graphtacular/tree/master/Graphtacular/Graphtacular/Saves) which are exporeted as [GEXF](https://gephi.org/gexf/format/index.html) (Graph Exchange XML Format) for display in [Gephi](https://gephi.org/)

## First rendering of a kernel generated motif:

![Flower](https://github.com/jasonb315/graphtacular/blob/master/assets/FirstChainRun.JPG) <br>
![Flower Gephi](https://github.com/jasonb315/graphtacular/blob/master/assets/FirstGephiCaptureFlower.JPG) <br>

## Driving clusters from the outside:

Operations on sets of Vertices

This is a logarithmic cluster with edges reflecting the repetative global split

![Logarithmic Cluster](https://github.com/jasonb315/graphtacular/blob/master/assets/LogarithmicCluster.JPG) <br>
![Logarithmic Cluster Gephi](https://github.com/jasonb315/graphtacular/blob/master/assets/LogarithmicClusterGephi.JPG) <br>

## Driving clusters from the inside:

Fibonacci:

![Logarithmic Cluster Gephi](https://github.com/jasonb315/graphtacular/blob/master/assets/Fibonacci.JPG) <br>

Other pictures in [assets >>>](https://github.com/jasonb315/graphtacular/tree/master/assets)

Additional graphs not pictured in [graphs >>>](https://github.com/jasonb315/graphtacular/tree/master/Graphtacular/Graphtacular/Saves)

***

## METHODS

| Graph Method | Use | Big O Time | Big O Space | IN | OUT |
| :----------- | :----------- | :-------------: | :-------------: | :-----------: | :-----------: |
| PrintMatrix | console output | O(n<sup>2</sup>) | O(n<sup>2</sup>) | - | void |
| PrintVertices | console output | O(n) | O(n) | - | void |
| PrintEdges | console output | O(n<sup>2</sup>) | O(n) | - | void |
| RegisterKey | associates a Vertex ID to a MatrixKey | O(1) | O(1) | Vertex | void |
| MatrixEntry | updates matrix with row and columb | O(n) | O(1) | Vertex | void |
| AddStrandVertex | vertex addition | O(1) | O(1) | - | Vertex |
| AddClusterVertex | vertex addition | O(1) | O(1) | - | Vertex |
| DeleteVertex | Removes Vertex from matrix, keys, list, count | O(1) | O(1) | Vertex | - |
| MatrixRemoval | Removes target Vertex from Matrix | O(n) | O(1) | Vertex | - |
| DeleteMatrixKey | Remove Vertex from MatrixKeys, mirror matrix contraction | O(n) | O(1) | Vertex | - |
| JoinVertexPairUndirected | Combines two Vertices on a new one, preserving their degrees | O(n) | O(1) | Vertex | - |
| DoppleSet | Mirrors a set of Vertices and their edges with a new set | O(n<sup>2</sup>) | O(n) | List Vertex | List Vertex |
| CreateClusterVertex | create and add Vertex; cluster Kernel (inert) | O(1) | O(1) | - | Vertex |
| CreateStrandVertex | create and add Vertex; strand Kernel | O(1) | O(1) | - | Vertex |
| AssimilateVertex | add, register, count, and enter matrix | O(1) | O(1) | Vertex | Vertex |
| AssimilateVertices | List Vertex to be added | O(n) | O(1) | List Vertex | void |
| DirectedEdge | directed edge between two Vertices | O(n) | O(n) | Vertex, Vertex, int | void |
| UndirectedEdge | undirected edge from one Vertex to another | O(1) | O(1) | Vertex, Vertex, int | void |
| OutDegreeCount | count and return edges from a Vertex | O(n) | O(1) | Vertex | int |
| OutDegreeVertices | collect and return vertices pointed to from a vertex | O(n) | O(n) | Vertex | List Vertex |
| InDegreeCount | count and return edges to a Vertex | O(n) | O(1) | Vertex | int |
| InDegreeVertices | collect and return vertices pointing to a vertex | O(n) | O(n) | Vertex | List Vertex |
| DirectedEdgeCount | sum of a Vertex indegree and outdegree | O(1) | O(1) | Vertex | int |
| MutualEdgeCount | for undirected, count mutual connections | O(1) | O(1) | Vertex | int |
| NeighborCount | direction agnostic neighbor count | O(1) | O(1) | Vertex | int |
| NeighborSet | direction agnostic neighbor list | O(1) | O(1) | Vertex | List Vertex |
| IslandCount | nonconnected Vertex count | O(n) | O(1) | - | int |
| IslandSet | nonconnected Vertex list | O(n) | O(n) | - | List Vertex |
| NeighborsWithinK | collect refrence to all Vertices within K of a target | O(n<sup>3</sup>) | O(n) | Vertex, int | List Vertex |
| MaxConnections | calculate the max potential global undirected edges for a given number of Vertices | O(n) | O(1) | int | int |
| IsSelfRefrence | check for edge to self | O(1) | O(1) | Vertex | bool |
| PurgeSelfRefrence | removes edge to self | O(n) | O(n) | Vertex | void |
| PurgeSelfReferences | removes multigraph, returns number removed | O(n<sup>2</sup>) | O(1) | - | int |
| FullConnectSet | complete undirected weighted connection for a set of Vertices to eachother | O(n<sup>2</sup>) | O(n) | List Vertex, int | LIST |
| ConnectionsBetweenCount | count the number of related Vertices in a set | O(n<sup>2</sup>) | O(n) | List Vertex | int |
| NondirectionalClusteringCoefficient | measures how connected a Vertex's neightobrs are to eachother | O(1) | O(1) | Vertex | int |

***

NOTE: Driver, Kernel, and GEXF file save prototypes are functional but incomplete, so their methods are not documented yet.

***
