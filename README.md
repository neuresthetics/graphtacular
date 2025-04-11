# GRAPHTACULAR

![Render](https://github.com/jasonb315/graphtacular/blob/master/assets/screenshot_100214.png)<br>

![Whiteboard](https://github.com/jasonb315/graphtacular/blob/master/assets/WB00.jpg) <br>

This Graph has Vertices that have Kernels which are given Strands..

Strands passed into Kernels, which contain a reference to the Vertex that is inside.

Vertices have references to the matrix they are inside.

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

The strand is passed to the Kernel, which parses it and calls logic on its shell Vertex:

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

![Flower](https://github.com/jasonb315/graphtacular/blob/master/assets/FirstChainRun.JPG) <br>
![Flower Gephi](https://github.com/jasonb315/graphtacular/blob/master/assets/FirstGephiCaptureFlower.JPG) <br>

## GEXF - save graph to file
You can view saved [graphs](https://github.com/jasonb315/graphtacular/tree/master/Graphtacular/Graphtacular/Saves) which are exported as [GEXF](https://github.com/neuresthetics/graphtacular/blob/master/Graphtacular/Graphtacular/Classes/Gexf.cs) (Graph Exchange XML Format) for display in [Gephi](https://gephi.org/)

### Building clusters from the outside:

Operations on sets of Vertices in clumps from the outside tend to produce more geological or crystal shapes.

This is a logarithmic cluster with edges reflecting the repetitive global split

![Logarithmic Cluster](https://github.com/jasonb315/graphtacular/blob/master/assets/LogarithmicCluster.JPG) <br>
![Logarithmic Cluster Gephi](https://github.com/jasonb315/graphtacular/blob/master/assets/LogarithmicClusterGephi.JPG) <br>

Does it feel kind of like ice, inorganic?

### Driving clusters from the inside:

Fibonacci:

![Logarithmic Cluster Gephi](https://github.com/jasonb315/graphtacular/blob/master/assets/Fibonacci.JPG) <br>

look feel has more body, more life?

Other pictures in [assets >>>](https://github.com/jasonb315/graphtacular/tree/master/assets)

The big round one has a look and feel of a wrinkly brain given its modular shape scrunched into space.

Additional graphs not pictured in [graphs >>>](https://github.com/jasonb315/graphtacular/tree/master/Graphtacular/Graphtacular/Saves)

- go to [TESTS](https://github.com/anoumenon/graphtacular/blob/master/Graphtacular/XUT/UnitTest1.cs) <br>

***

## METHODS

| Graph Method | Use | Big O Time | Big O Space | IN | OUT |
| :----------- | :----------- | :-------------: | :-------------: | :-----------: | :-----------: |
| PrintMatrix | console output | O(n<sup>2</sup>) | O(n<sup>2</sup>) | - | void |
| PrintVertices | console output | O(n) | O(n) | - | void |
| PrintEdges | console output | O(n<sup>2</sup>) | O(n) | - | void |
| RegisterKey | associates a Vertex ID to a MatrixKey | O(1) | O(1) | Vertex | void |
| MatrixEntry | updates matrix with row and column | O(n) | O(1) | Vertex | void |
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
| NeighborsWithinK | collect reference to all Vertices within K of a target | O(n<sup>3</sup>) | O(n) | Vertex, int | List Vertex |
| MaxConnections | calculate the max potential global undirected edges for a given number of Vertices | O(n) | O(1) | int | int |
| IsSelfRefrence | check for edge to self | O(1) | O(1) | Vertex | bool |
| PurgeSelfRefrence | removes edge to self | O(n) | O(n) | Vertex | void |
| PurgeSelfReferences | removes multigraph, returns number removed | O(n<sup>2</sup>) | O(1) | - | int |
| FullConnectSet | complete undirected weighted connection for a set of Vertices to each other | O(n<sup>2</sup>) | O(n) | List Vertex, int | LIST |
| ConnectionsBetweenCount | count the number of related Vertices in a set | O(n<sup>2</sup>) | O(n) | List Vertex | int |
| NondirectionalClusteringCoefficient | measures how connected a Vertex's neighbors are to each other | O(1) | O(1) | Vertex | int |

big O time and space does not include dependent functions, such as NondirectionalClusteringCoefficient simply:
```
return decimal.Divide(ConnectionsBetweenCount(n), MaxConnections(n.Count()));
```
both of which have their own runtime complexity..

***
