using Graphtacular.Classes;
using Graphtacular.Classes.Drivers;
using Graphtacular.Classes.Strands;
using Graphtacular.Classes.Motifs;
using System;
using System.Collections.Generic;

namespace Graphtacular
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Graph graph = new Graph();
            for (int i = 0; i < 11; i++)
            {
                graph.AddStrandVertex();
            }
            DriverZero D = new DriverZero();
            D.FormRing(graph, graph.AllVertices, 1);


            //// FLOWER RUN //
            //StrandLib strand = new StrandLib();
            //// instantiate graph
            //Graph graph = new Graph();
            //// seed graph
            //var zero = graph.AddStrandVertex();
            //// run seed
            //zero.K.Run(strand.Flower(), 0);

            
            Gexf gexf = new Gexf();
            gexf.SaveGraph(graph, "undirected", "string");


            graph.PrintMatrix();
            graph.PrintEdges();
            Console.WriteLine($"SIZE: {graph.size}");
            graph.PrintVertices();
        }
    }
}
