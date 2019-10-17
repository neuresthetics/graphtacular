using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Graphtacular.Classes
{
    public class Gexf
    {
        /// <summary>
        ///     Export to GEXF
        /// </summary>
        /// <param name="graph">Graph graph instance</param>
        /// <param name="defaultedgetype">"directed", "undirected(!supportedYet)"</param>
        /// <param name="idtype">"static"</param>
        public void SaveGraph(Graph graph, string defaultedgetype, string idtype)
        {
            StringBuilder output = new StringBuilder();

            // headers
            output.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            output.AppendLine("<gexf xmlns:viz=\"http:///www.gexf.net/1.1draft/viz\" version=\"1.1\" xmlns=\"http://www.gexf.net/1.1draft\">");

            // meta data
            DateTime date = DateTime.Now;
            int? preZero = null;
            if(date.Month < 10) preZero = 0;

            output.AppendLine($"<meta lastmodifieddate=\"{date.Year}-{preZero}{date.Month}-{date.Day}+{date.Hour}:{date.Second}\">");


            output.AppendLine("<creator>Jason Burns</creator>");
            output.AppendLine("</meta>");

            // open graph
            output.AppendLine($"<graph defaultedgetype=\"{defaultedgetype}\" idtype=\"{idtype}\" type=\"static\">");

            // open nodes
            output.AppendLine($"<nodes count=\"{graph.size.ToString()}\">");

            // write nodes
            foreach (var vert in graph.AllVertices)
            {
                output.AppendLine($"<node id=\"{graph.MatrixKey[vert.ID]}.0\" label=\"{vert.ID}\"/>");
            }

            // close nodes
            output.AppendLine("</nodes>");

            // open edges
            output.AppendLine($"<edges count=\"{graph.edgeCount.ToString()}\">");

            // read write edges
            int tempID = 0;
            for (int i = 0; i < graph.Matrix.Count; i++)
            {
                for (int j = 0; j < graph.Matrix.Count; j++)
                {
                    if (graph.Matrix[i][j] != 0)
                    {
                        output.AppendLine(
                            $"<edge id=\"{tempID}\" source=\"{graph.MatrixKey[graph.AllVertices[i].ID]}.0\" target=\"{graph.MatrixKey[graph.AllVertices[j].ID]}.0\"  weight=\"{graph.Matrix[i][j]}.0\" />");
                        tempID++;
                    }
                }
            }

            // close edges
            output.AppendLine("</edges>");
            // close graph
            output.AppendLine("</graph>");
            // close file
            output.AppendLine("</gexf>");


            Guid guid = Guid.NewGuid();

            string path = ($"../../../Saves/graph-{guid.ToString()}.gexf");

            using (StreamWriter sw = File.CreateText(path))
            {
                    sw.WriteLine(output);
            }
        }
    }
}
