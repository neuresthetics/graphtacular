using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphtacular.Classes.Motifs
{
    // non-strand algo
    class SierpinskiTriangleGenerator
    {
        Graph matrix { get; set; }

        // tier collective
        List<Vertex> sub0 = new List<Vertex>();
        Vertex T0 { get; set; }
        Vertex a0 { get; set; }
        Vertex b0 { get; set; }
        Vertex c0 { get; set; }

        // tier additions; mirrors of tier collective
        List<Vertex> sub1 = new List<Vertex>();
        Vertex T1 { get; set; }
        Vertex a1 { get; set; }
        Vertex b1 { get; set; }
        Vertex c1 { get; set; }

        List<Vertex> sub2 = new List<Vertex>();
        Vertex T2 { get; set; }
        Vertex a2 { get; set; }
        Vertex b2 { get; set; }
        Vertex c2 { get; set; }

        List<Vertex> sub3 = new List<Vertex>();
        Vertex T3 { get; set; }
        Vertex a3 { get; set; }
        Vertex b3 { get; set; }
        Vertex c3 { get; set; }

        public SierpinskiTriangleGenerator(Graph graph)
        {
            matrix = graph;
            Ini();
        }

        public void Ini()
        {
            T0 = matrix.AddStrandVertex();
            sub0.Add(T0);
            a0 = matrix.AddStrandVertex();
            sub0.Add(a0);
            b0 = matrix.AddStrandVertex();
            sub0.Add(b0);
            c0 = matrix.AddStrandVertex();
            sub0.Add(c0);
            matrix.FullConnectSet(sub0, 1);
        }

        public Graph Generate(int levels)
        {
            if (levels == 1) return matrix;

            while (levels > 1)
            {
                // make three !copies but mirrors with unique guid
                // match vertex count across duplicates
                sub1 = matrix.DoppleSet(sub0);
                sub2 = matrix.DoppleSet(sub0);
                sub3 = matrix.DoppleSet(sub0);

                // spatial orientation
                foreach (var vert in sub1)
                {
                    if (matrix.OutDegreeCount(vert) == 3)
                    {
                        if (T1 == null) T1 = vert;

                        else if(a1 == null) a1 = vert;

                        else if (b1 == null) b1 = vert;

                        else if (c1 == null) b1 = vert;
                    }
                }

                foreach (var vert in sub2)
                {
                    if (matrix.OutDegreeCount(vert) == 3)
                    {
                        if (T2 == null) T2 = vert;

                        else if (a2 == null) a2 = vert;

                        else if (b2 == null) b2 = vert;

                        else if (c2 == null) b2 = vert;
                    }
                }

                foreach (var vert in sub3)
                {
                    if (matrix.OutDegreeCount(vert) == 3)
                    {
                        if (T3 == null) T3 = vert;

                        else if (a3 == null) a3 = vert;

                        else if (b3 == null) b3 = vert;

                        else if (c3 == null) b3 = vert;
                    }
                }

                // ! bug warning
                // connect copies according to orientation schema
                matrix.PrintMatrix();

                matrix.JoinVertexPairUndirected(a1, b2);
                matrix.JoinVertexPairUndirected(a2, b3);
                matrix.JoinVertexPairUndirected(a3, b1);

                matrix.JoinVertexPairUndirected(a0, T1);
                matrix.JoinVertexPairUndirected(b0, T2);
                matrix.JoinVertexPairUndirected(c0, T3);


                List<Vertex> sub0edit = new List<Vertex>(sub0);
                foreach (var vert in sub0edit) if (vert == a0 || vert == b0 || vert == c0) sub0.Remove(vert);

                List<Vertex> sub1edit = new List<Vertex>(sub1);
                foreach (var vert in sub1edit) if (vert == a1 || vert == b1 || vert == T1) sub1.Remove(vert);

                List<Vertex> sub2edit = new List<Vertex>(sub2);
                foreach (var vert in sub2edit) if (vert == a2 || vert == b2 || vert == T2) sub2.Remove(vert);

                List<Vertex> sub3edit = new List<Vertex>(sub3);
                foreach (var vert in sub3edit) if (vert == a3 || vert == b3 || vert == T3) sub3.Remove(vert);


                // combine all subs into sub0
                //sub0.Concat(sub1).Concat(sub2).Concat(sub3).ToList();

                foreach (var vert in sub1) sub0.Add(vert);
                foreach (var vert in sub2) sub0.Add(vert);
                foreach (var vert in sub3) sub0.Add(vert);


                sub1.Clear();
                sub2.Clear();
                sub3.Clear();
                // reassign sub0 orientation refs
                // clear sub0+ lists/refs
                a0 = c1; 
                b0 = c2; 
                c0 = c3;


                c1 = null;
                c2 = null;
                c3 = null;

                a1 = null;
                a2 = null;
                a3 = null;

                b1 = null;
                b2 = null;
                b3 = null;

                T1 = null;
                T2 = null;
                T2 = null;

                // T0 is never cleared; remains cap

                // repeat
                levels--;
            }
            return matrix; // code holder
        }
    }
}
