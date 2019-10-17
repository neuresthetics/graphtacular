using System;
using System.Collections.Generic;
using System.Text;

namespace Graphtacular.Classes.Strands
{
    public class StrandLib
    {
        // FORMAT
        // BASE: public Dictionary<int, object[]> x = new Dictionary<int, object[]>();
        // RUNG: {strand}.Add(n, new object[] { prop, ... });

        public Dictionary<int, object[]> Flower()
        {
            Dictionary<int, object[]> flower = new Dictionary<int, object[]>();
            int n = 1;

            flower.Add(n, new object[] { "KcompleteCluster", 8, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KcompleteCluster", 8, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KcompleteCluster", 8, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;
            flower.Add(n, new object[] { "KbranchUndirected", 1, 1 }); n++;


            flower.Add(n, new object[] { null });
            return flower;
        }
    }
}
